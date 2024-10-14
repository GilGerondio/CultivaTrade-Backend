using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public Guid CategoryId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
