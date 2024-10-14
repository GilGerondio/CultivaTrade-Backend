using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            Deliveries = new HashSet<Delivery>();
            Feedbacks = new HashSet<Feedback>();
        }

        [Key]
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid DeliveryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDateReceived { get; set; }
        [StringLength(50)]
        public string OrderStatus { get; set; } = null!;

        [ForeignKey("BuyerId")]
        [InverseProperty("OrderBuyers")]
        public virtual User Buyer { get; set; } = null!;
        [ForeignKey("DeliveryId")]
        [InverseProperty("Orders")]
        public virtual Delivery Delivery { get; set; } = null!;
        [ForeignKey("ProductId")]
        [InverseProperty("Orders")]
        public virtual Product Product { get; set; } = null!;
        [ForeignKey("SellerId")]
        [InverseProperty("OrderSellers")]
        public virtual User Seller { get; set; } = null!;
        [InverseProperty("Order")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
