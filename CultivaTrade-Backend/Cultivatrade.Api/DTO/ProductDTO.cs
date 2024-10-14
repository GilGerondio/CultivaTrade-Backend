using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cultivatrade.Api.DTO
{

    public class ProductDTO_GET
    {
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public string SellerFirstname { get; set; }
        public string SellerLastname { get; set; }
        public string SellerEmail { get; set; }
        public string SellerPhone { get; set; }
        public string SellerAddress { get; set; }
        public string SellerImage { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    public class ProductDTO_POST
    {
        public Guid SellerId { get; set; }
        public Guid CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
