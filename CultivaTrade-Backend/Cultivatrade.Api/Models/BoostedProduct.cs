using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cultivatrade.Api.Models
{
    [Table("BoostedProduct")]
    public partial class BoostedProduct
    {
        [Key]
        public Guid BoostedProductId { get; set; }
        public Guid ProductId { get; set; }
        public double BoostCost { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("BoostedProducts")]
        public virtual Product Product { get; set; } = null!;
    }
}
