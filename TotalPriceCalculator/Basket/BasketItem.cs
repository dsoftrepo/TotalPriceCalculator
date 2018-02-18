namespace TotalPriceCalculator.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal LastPrice { get; set; }
    }
}
