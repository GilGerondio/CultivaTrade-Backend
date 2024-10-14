using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Delivery")]
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public Guid DeliveryId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        [StringLength(50)]
        public string DeliveryStatus { get; set; } = null!;
        [StringLength(50)]
        public string DeliveryOption { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DeliveryTime { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Deliveries")]
        public virtual User Buyer { get; set; } = null!;
        [ForeignKey("OrderId")]
        [InverseProperty("Deliveries")]
        public virtual Order Order { get; set; } = null!;
        [InverseProperty("Delivery")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
