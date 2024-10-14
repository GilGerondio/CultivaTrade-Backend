using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            BoostedProducts = new HashSet<BoostedProduct>();
            Carts = new HashSet<Cart>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            ProductFiles = new HashSet<ProductFile>();
        }

        [Key]
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey("SellerId")]
        [InverseProperty("Products")]
        public virtual User Seller { get; set; } = null!;
        [InverseProperty("Product")]
        public virtual ICollection<BoostedProduct> BoostedProducts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }
}
