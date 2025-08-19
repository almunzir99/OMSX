using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.ProductsService.Entities
{
    public class ProductAttribute : EntityBase
    {
        public Guid NameId { get; set; }

        public Guid ValueId { get; set; }

        public Guid ProductId { get; set; }
        [Column(nameof(ProductId))]
        public Product? Product { get; set; }

        [ForeignKey(nameof(NameId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public required virtual Localization Name { get; set; }

        [ForeignKey(nameof(ValueId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public required virtual Localization Value { get; set; }
    }
}