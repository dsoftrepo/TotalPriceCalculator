using System;
using System.Collections.Generic;
using System.Linq;
using TotalPriceCalculator.Calculator;

namespace TotalPriceCalculator.Basket
{
    public class Basket
    {
        private readonly List<BasketItem> _items;
        private readonly IBasketPriceCalculator _calculator;

        public Basket(IBasketPriceCalculator calculator)
        {
            TotalPrice = 0;
            _items = new List<BasketItem>();
            _calculator = calculator;
        }

        public IEnumerable<BasketItem> Items {
            get { return _items; }
        }

        public decimal TotalPrice { get; private set; }

        public void AddItem(BasketItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            
            var existing = FindItemInBasket(item.Product);
            if (existing!=null)
            {
                UpdateQuantity(item, existing.Quantity + item.Quantity);
                return;
            }

            _calculator.ApplyDiscount(item);
            _items.Add(item);
            RecalculateTotal();
        }

        public void RemoveItem(BasketItem item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
            RecalculateTotal();
        }

        public void UpdateQuantity(BasketItem item, int qty)
        {
            var basketItem = FindItemInBasket(item);
            if (basketItem == null) return;

            basketItem.Quantity = qty;
            _calculator.ApplyDiscount(basketItem);
            RecalculateTotal();
        }

        private BasketItem FindItemInBasket(BasketItem item)
        {
            return _items.FirstOrDefault(x=>x.Id == item.Id);
        }

        private BasketItem FindItemInBasket(Product product)
        {
            return _items.FirstOrDefault(x => x.Product.Id == product.Id);
        }

        private void RecalculateTotal()
        {
            TotalPrice = _calculator.CalculateTotal(_items);
        }
    }
}
