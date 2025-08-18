using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.ProductsService.Entities
{
    // Product Options 

    public class ProductOptions : AuditEntityBase
    {
        public Guid NameId { get; set; }

        public Guid? DescriptionId { get; set; }

        public decimal PriceModifier { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [ForeignKey(nameof(NameId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public required virtual Localization Name { get; set; }

        [ForeignKey(nameof(DescriptionId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Localization? Description { get; set; }
    }
}