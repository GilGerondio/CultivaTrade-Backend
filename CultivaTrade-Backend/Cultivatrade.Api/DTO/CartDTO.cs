namespace Cultivatrade.Api.DTO
{
    public class CartDTO_POST
    {
        public Guid BuyerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
       
    }
}
