using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Cultivatrade.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductProfileController : Controller
    {
        private readonly ProductFileLogic _productFileLogic; 
        public ProductProfileController(ProductFileLogic productFileLogic)
        {
            _productFileLogic = productFileLogic;
        }

        [HttpGet("{productId}")]
        public IActionResult GetProductFileById(Guid productId)
        {
            try
            {
                var data = _productFileLogic.GetProductFileById(productId);
                return new FileStreamResult(data.FileStream, data.ContentType);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        
        [HttpPost]
        public IActionResult AddProductFile([FromForm] ProductFileDTO_POST dto)
        {
            try
            {
                bool isSuccess = _productFileLogic.AddProductFile(dto);
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
