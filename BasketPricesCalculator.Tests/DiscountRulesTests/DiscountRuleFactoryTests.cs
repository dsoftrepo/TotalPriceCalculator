using System;
using NUnit.Framework;
using TotalPriceCalculator.DiscountRules;

namespace BasketPricesCalculator.Tests.DiscountRulesTests
{
    [TestFixture]
    public class DiscountRuleFactoryTests
    {
        [Test]
        public void GivenDiscountType_WhenResolveDiscountRule_ThenReturnsCorrectRuleInstance()
        {
            var factory = new DiscountRuleFactory();
            var discountRule = factory.ResolveDiscountRule(DiscountRuleType.None);
            Assert.AreEqual(typeof(DefaultRule), discountRule.GetType());
        }

        [Test]
        public void GivenDiscountType_WhenResolveDiscountRule_ThenReturnsCorrectRuleInstance_2()
        {
            var factory = new DiscountRuleFactory();
            var discountRule = factory.ResolveDiscountRule(DiscountRuleType.BuyManyGetOneFree);
            Assert.AreEqual(typeof(BuyManyGetOneFree), discountRule.GetType());
        }

        [Test]
        public void GivenDiscountType_WhenResolveDiscountRule_ThenReturnsCorrectRuleInstance_3()
        {
            var factory = new DiscountRuleFactory();
            var discountRule = factory.ResolveDiscountRule(DiscountRuleType.Percentage);
            Assert.AreEqual(typeof(PercentageRule), discountRule.GetType());
        }

        [Test]
        public void GivenWrongDiscountType_WhenResolveDiscountRule_ThenThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var factory = new DiscountRuleFactory();
                factory.ResolveDiscountRule((DiscountRuleType)4);
            });
        }
    }
}
