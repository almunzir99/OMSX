using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.ProductsService.Entities
{
    public class ProductImage : EntityBase
    {
        public Guid ProductId { get; set; }
        public Guid ImageId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
        public virtual Image Image { get; set; } = null!;

    }
}