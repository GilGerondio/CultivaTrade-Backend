using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;

namespace Cultivatrade.Api.Logics
{
    public class CartLogic
    {
        private readonly CultivatradeContext _context;
        public CartLogic(CultivatradeContext context)
        {
            _context = context;
        }

        // ADD TO CART
        public bool AddCart(CartDTO_POST dto)
        {
            int success = 0;
            var data = new Cart();

            data.CartId = Guid.NewGuid();
            data.BuyerId = dto.BuyerId;
            data.ProductId = dto.ProductId;
            data.Quantity = dto.Quantity;
            data.DateTimeCreated = DateTime.UtcNow;

            _context.Carts.Add(data);
            success = _context.SaveChanges();

            if(success > 0)
            {
                return true;
            }
            return false;
        }
    }
}
