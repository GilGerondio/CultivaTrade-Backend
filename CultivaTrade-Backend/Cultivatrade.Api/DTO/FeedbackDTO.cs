using System.ComponentModel.DataAnnotations.Schema;

namespace Cultivatrade.Api.DTO
{
    public class FeedbackDTO_GET
    {
    }
    public class FeedbackDTO_POST
    {
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        public int Rating { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
