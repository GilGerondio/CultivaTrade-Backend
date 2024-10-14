using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        public int Rating { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Feedbacks")]
        public virtual User Buyer { get; set; } = null!;
        [ForeignKey("OrderId")]
        [InverseProperty("Feedbacks")]
        public virtual Order Order { get; set; } = null!;
    }
}
