using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Cultivatrade.Api.Controllers
{
    public class CartController : Controller
    {
        private readonly CartLogic _cartLogic;
        private readonly CultivatradeContext _context;

        public CartController(CartLogic cartLogic, CultivatradeContext context)
        {
            _cartLogic = cartLogic;
            _context = context;
        }

        // ADD TO CART
        public IActionResult AddCart(CartDTO_POST dto)
        {
            try
            {
                bool isSuccess = _cartLogic.AddCart(dto);
                if (isSuccess)
                {
                    return Ok("success");
                }
                return Json("error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
