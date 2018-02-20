using System;
using System.Linq;
using NUnit.Framework;
using TotalPriceCalculator.Basket;
using TotalPriceCalculator.Calculator;
using TotalPriceCalculator.DiscountRules;

namespace BasketPricesCalculator.Tests.BasketTests
{
    [TestFixture]
    public class BasketTests
    {
        [Test]
        public void GivenNullBasketItem_WhenAddItem_ThenThrowsException()
        {
            var basket = GetBasketInstance();
            Assert.Throws<ArgumentNullException>(() =>
            {
                basket.AddItem(null);
            });
        }

        [Test]
        public void GivenBasket_WhenAddItem_ThenTotalPriceIsUpdated()
        {
            var basket = GetBasketInstance();

            var item = new BasketItem
            {
                Quantity = 2,
                AfterDiscountPrice = 0,
                DiscountRuleType = DiscountRuleType.BuyManyGetOneFree,
                DiscountParameter = 2,
                Product = new Product
                {
                    Price = (decimal)10.50
                }
            };

            basket.AddItem(item);

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual(10.50, basket.TotalPrice);
        }

        [Test]
        public void GivenBasket_WhenAddItems_ThenTotalPriceIsUpdated()
        {
            var basket = GetBasketInstance();

            basket.AddItem(GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2));

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)10.50, basket.TotalPrice);

            basket.AddItem(GetBasketItem(2, 2, DiscountRuleType.Percentage, 10));

            Assert.AreEqual(2, basket.Items.Count());
            Assert.AreEqual((decimal)29.40, basket.TotalPrice);

            basket.AddItem(GetBasketItem(3, 2, DiscountRuleType.None, 0));

            Assert.AreEqual(3, basket.Items.Count());
            Assert.AreEqual((decimal)50.40, basket.TotalPrice);
        }

        [Test]
        public void GivenBasket_WhenAddSameIdItems_ThenTotalPriceIsUpdated()
        {
            var basket = GetBasketInstance();

            basket.AddItem(GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2));

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)10.50, basket.TotalPrice);

            basket.AddItem(GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2));

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual(4, basket.Items.ToList()[0].Quantity);
            Assert.AreEqual((decimal)21.00, basket.TotalPrice);

            basket.AddItem(GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2));

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual(6, basket.Items.ToList()[0].Quantity);
            Assert.AreEqual((decimal)31.50, basket.TotalPrice);
        }

        [Test]
        public void GivenBasketItems_WhenUpdateQuantity_ThenTotalPriceIsUpdated()
        {
            var basket = GetBasketInstance();

            var item = GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2);
            basket.AddItem(item);
            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)10.50, basket.TotalPrice);

            basket.UpdateQuantity(item, 4);

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)21.00, basket.TotalPrice);

            basket.UpdateQuantity(item, 3);

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)21.00, basket.TotalPrice);
        }

        [Test]
        public void GivenBasketItems_WhenRemoveItem_ThenTotalPriceIsUpdated()
        {
            var basket = GetBasketInstance();
            var item = GetBasketItem(1, 2, DiscountRuleType.BuyManyGetOneFree, 2);
            basket.AddItem(item);
            var item2 = GetBasketItem(2, 2, DiscountRuleType.Percentage, 10);
            basket.AddItem(item2);
            var item3 = GetBasketItem(3, 2, DiscountRuleType.None, 0);
            basket.AddItem(item3);

            Assert.AreEqual(3, basket.Items.Count());
            Assert.AreEqual((decimal)50.40, basket.TotalPrice);

            basket.RemoveItem(item3);

            Assert.AreEqual(2, basket.Items.Count());
            Assert.AreEqual((decimal)29.40, basket.TotalPrice);

            basket.RemoveItem(item3);

            Assert.AreEqual(2, basket.Items.Count());
            Assert.AreEqual((decimal)29.40, basket.TotalPrice);

            basket.RemoveItem(item2);

            Assert.AreEqual(1, basket.Items.Count());
            Assert.AreEqual((decimal)10.50, basket.TotalPrice);

            basket.RemoveItem(item);

            Assert.AreEqual(0, basket.Items.Count());
            Assert.AreEqual(0, basket.TotalPrice);
        }

        private BasketItem GetBasketItem(int id, int qty, DiscountRuleType type, int param)
        {
            return new BasketItem
            {
                Id = id,
                Quantity = qty,
                AfterDiscountPrice = 0,
                DiscountRuleType = type,
                DiscountParameter = param,
                Product = new Product
                {
                    Id = id,
                    Price = (decimal)10.50,
                }
            };
        }

        private static Basket GetBasketInstance()
        {
            var discountFactory = new DiscountRuleFactory();
            var calculator = new BasketPriceCalculator(discountFactory);
            return new Basket(calculator);
        }
    }
}
