using Cultivatrade.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Cultivatrade.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryLogic _categoryLogic;
        public CategoryController(CategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        // GET CATEGORY
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                var data = await _categoryLogic.GetCategory();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
