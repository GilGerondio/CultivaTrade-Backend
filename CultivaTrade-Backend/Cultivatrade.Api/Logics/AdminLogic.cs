using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Cultivatrade.Api.Logics
{
    public class AdminLogic
    {
        private readonly CultivatradeContext _context;
        public AdminLogic(CultivatradeContext context)
        {
            _context = context;
        }

        // LOGIN ADMIN
        public async Task<Admin> LoginAdmin(AdminDTO_LOGIN dto)
        {
            var data = await _context.Admins.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if (data != null)
            {
                if (BC.Verify(dto.Password, data.Password))
                {
                    return data;
                }
                return null;
            }
            return null;
        }
    }
}
