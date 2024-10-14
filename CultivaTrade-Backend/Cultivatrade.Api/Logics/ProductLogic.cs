using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Logics
{
    public class ProductLogic
    {
        private readonly CultivatradeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductLogic(CultivatradeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET PRODUCT
        public async Task<List<ProductDTO_GET>> GetProduct(string? search = "")
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                        join u in _context.Users on p.SellerId equals u.UserId
                        select new ProductDTO_GET
                        {
                            ProductId = p.ProductId,
                            SellerId = p.SellerId,
                            CategoryId = p.CategoryId,
                            SellerFirstname = u.Firstname,
                            SellerLastname = u.Lastname,
                            SellerEmail = u.Email,
                            SellerPhone = u.Phone,
                            SellerAddress = u.Address,
                            CategoryName = c.Name,
                            ProductName = p.Name,
                            ProductDescription = p.Description,
                            ProductPrice = p.Price,
                            Quantity = p.Quantity,
                            ExpiryDate = p.ExpiryDate,
                            SellerImage = Convert.ToBase64String(System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Images/Profiles/{u.ProfileImage}")),
                        };

            var data = await query.Where(x => string.IsNullOrWhiteSpace(search) || x.ProductName.Contains(search)).ToListAsync();

            data.ForEach(item =>
            {
                item.SellerImage = $"data:image/png;base64,{item.SellerImage}";
            });

            return data;
        }

        // GET PRODUCT BY PRODUCT ID
        public async Task<ProductDTO_GET> GetProductByProductId(Guid productId)
        {
            var data = await (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId
                            join u in _context.Users on p.SellerId equals u.UserId
                            where p.ProductId == productId
                            select new ProductDTO_GET
                            {
                                ProductId = p.ProductId,
                                SellerId = p.SellerId,
                                CategoryId = p.CategoryId,
                                SellerFirstname = u.Firstname,
                                SellerLastname = u.Lastname,
                                SellerEmail = u.Email,
                                SellerPhone = u.Phone,
                                SellerAddress = u.Address,
                                CategoryName = c.Name,
                                ProductName = p.Name,
                                ProductDescription = p.Description,
                                ProductPrice = p.Price,
                                Quantity = p.Quantity,
                                ExpiryDate = p.ExpiryDate,
                                SellerImage = Convert.ToBase64String(System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Images/Profiles/{u.ProfileImage}")),
                            }).FirstOrDefaultAsync();

                    if (data != null)
                    {
                        data.SellerImage = $"data:image/png;base64,{data.SellerImage}";
                    }

            return data;
        }

        // ADD PRODUCT
        public async Task<bool> AddProduct(ProductDTO_POST dto)
        {
            int success = 0;

            var data = new Product();
            data.ProductId = Guid.NewGuid();
            data.SellerId = dto.SellerId;
            data.CategoryId = dto.CategoryId;
            data.Name = dto.ProductName;
            data.Description = dto.ProductDescription;
            data.Price = dto.ProductPrice;
            data.Quantity = dto.Quantity;
            data.ExpiryDate = dto.ExpiryDate;
            data.DateTimeCreated = DateTime.UtcNow;

            _context.Products.Add(data);

            success = await _context.SaveChangesAsync();
            if(success > 0)
            {
                return true;
            }
            return false;
        }
    }
}
