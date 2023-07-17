namespace Tambolo.Dtos
{
    public class CartDetailsResponse
    {
        public CartHeaderResponse CartHeader { get; set; }
        public IEnumerable<CartResponse> Cart { get; set; }
    }
}
