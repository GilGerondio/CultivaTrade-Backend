using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Cultivatrade.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly FeedbackLogic _feedbackLogic;
        public FeedbackController(FeedbackLogic feedbackLogic)
        {
            _feedbackLogic = feedbackLogic;
        }

        // ADD FEEDBACK
        [HttpPost]
        public async Task<IActionResult> AddFeeback(FeedbackDTO_POST dto)
        {
            try
            {
                bool isSuccess = await _feedbackLogic.AddFeedback(dto);
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
