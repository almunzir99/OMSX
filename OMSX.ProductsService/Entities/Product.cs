using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.ProductsService.Entities
{
    public class Product : AuditEntityBase
    {
        public required virtual Guid VendorId { get; set; }

        public required Guid ProductNameId { get; set; }
        public required Guid DescriptionId { get; set; }

        public required Guid GategoryId { get; set; }

        public double Price { get; set; }

        [Column(nameof(GategoryId))]
        public Category? Category { get; set; }

        [ForeignKey(nameof(ProductNameId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public required virtual Localization ProductName { get; set; }

        [ForeignKey(nameof(DescriptionId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public required virtual Localization Description { get; set; }

        public List<ProductImage> ProductImages { get; set; } = new();
        public List<ProductAttribute> ProductAttributes { get; set; } = new();
        public List<ProductOptions> ProductOptions { get; set; } = new();
    }
}