using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Logics
{
    public class CategoryLogic
    {
        private readonly CultivatradeContext _context;
        public CategoryLogic(CultivatradeContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO_GET>> GetCategory()
        {
            var data = from c in _context.Categories
                       select new CategoryDTO_GET
                       {
                           CategoryId = c.CategoryId,
                           CategoryName = c.Name
                       };

            return await data.ToListAsync();
        }
    }
}
