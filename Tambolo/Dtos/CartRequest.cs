namespace Tambolo.Dtos
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
