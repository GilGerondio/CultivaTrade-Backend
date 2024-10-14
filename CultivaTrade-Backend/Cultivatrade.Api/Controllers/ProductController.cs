using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Logics;
using Microsoft.AspNetCore.Mvc;
using VisitorSystem.Api.Logics;
using VisitorSystem.Api.Models;

namespace Cultivatrade.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductLogic _productLogic;
        public ProductController(ProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        // GET PRODUCT
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] PaginationRequest? paginationRequest, [FromQuery] string? search = "")
        {
            try
            {
                var data = await _productLogic.GetProduct(search);
                var pagination = PaginationLogic.PaginateData(data, paginationRequest);
                return Ok(pagination);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        // GET PRODUCT BY PRODUCT ID
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByProductId(Guid productId)
        {
            try
            {
                var data = await _productLogic.GetProductByProductId(productId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        // ADD PRODUCT
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO_POST dto)
        {
            try
            {
                bool isSuccess = await _productLogic.AddProduct(dto);
                if (isSuccess)
                {
                    return Ok("success");
                }
                return Json("error");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
