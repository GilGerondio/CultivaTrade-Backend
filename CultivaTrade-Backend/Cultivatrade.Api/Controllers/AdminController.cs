using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Logics;
using Cultivatrade.Api.Models;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
namespace Cultivatrade.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly AdminLogic _adminLogic;
        private readonly CultivatradeContext _context;
        
        public AdminController(AdminLogic adminLogic, CultivatradeContext context)
        {
            _adminLogic = adminLogic;
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAdmin(AdminDTO_LOGIN dto)
        {
            try
            {
                var data = await _adminLogic.LoginAdmin(dto);
                if(data != null)
                {
                    return Ok(data);
                }
                return Json("error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        //[HttpPost]
        //public IActionResult AddAdmin(string username, string password)
        //{
        //    var data = new Admin();
        //    data.AdminId = Guid.NewGuid();
        //    data.Username = username;
        //    data.Password = BC.HashPassword(password, BC.GenerateSalt());
        //    data.DateTimeCreated = DateTime.UtcNow;

        //    _context.Admins.Add(data);
        //    _context.SaveChanges();
        //    return Ok("success");

        //}

    }
}
