using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ProductId { get; set; }
        [StringLength(50)]
        public string Message { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Notifications")]
        public virtual User Buyer { get; set; } = null!;
        [ForeignKey("ProductId")]
        [InverseProperty("Notifications")]
        public virtual Product Product { get; set; } = null!;
    }
}
