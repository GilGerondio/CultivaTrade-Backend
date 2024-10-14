﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Deliveries = new HashSet<Delivery>();
            Feedbacks = new HashSet<Feedback>();
            Notifications = new HashSet<Notification>();
            OrderBuyers = new HashSet<Order>();
            OrderSellers = new HashSet<Order>();
            Products = new HashSet<Product>();
        }

        [Key]
        public Guid UserId { get; set; }
        [StringLength(50)]
        public string Firstname { get; set; } = null!;
        [StringLength(50)]
        public string Lastname { get; set; } = null!;
        [StringLength(50)]
        public string Phone { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        [StringLength(50)]
        public string? ProfileImage { get; set; }
        [StringLength(50)]
        public string? BusinessPermitNumber { get; set; }
        [StringLength(50)]
        public string? BusinessPermitImage { get; set; }
        [StringLength(50)]
        public string? SanitaryPermitImage { get; set; }
        public bool IsSeller { get; set; }
        public bool IsApproved { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTimeCreatedAsSeller { get; set; }
        public int? VerificationCode { get; set; }

        [InverseProperty("Buyer")]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty("Buyer")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        [InverseProperty("Buyer")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [InverseProperty("Buyer")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("Buyer")]
        public virtual ICollection<Order> OrderBuyers { get; set; }
        [InverseProperty("Seller")]
        public virtual ICollection<Order> OrderSellers { get; set; }
        [InverseProperty("Seller")]
        public virtual ICollection<Product> Products { get; set; }
    }
}