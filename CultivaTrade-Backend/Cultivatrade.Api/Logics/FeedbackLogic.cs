using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;

namespace Cultivatrade.Api.Logics
{
    public class FeedbackLogic
    {
        private readonly CultivatradeContext _context;
        public FeedbackLogic(CultivatradeContext context)
        { 
            _context = context;
        }

        public async Task<bool> AddFeedback(FeedbackDTO_POST dto)
        {
            int success = 0;
            var data = new Feedback();
            data.FeedbackId = Guid.NewGuid();
            data.BuyerId = dto.BuyerId;
            data.OrderId = dto.OrderId;
            data.Rating = dto.Rating;
            data.DateTimeCreated = DateTime.UtcNow;

            _context.Feedbacks.Add(data);
            success = await _context.SaveChangesAsync();
            
            if(success > 0)
            {
                return true;
            }
            return false;
        }

    }
}
