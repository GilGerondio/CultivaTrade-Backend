namespace Cultivatrade.Api.DTO
{
    public class ProductFileDTO_POST
    {
        public Guid ProductId { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    
}
