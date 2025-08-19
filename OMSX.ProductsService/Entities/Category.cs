using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.ProductsService.Entities
{
    public class Category : AuditEntityBase
    {
        public Guid ParentId { get; set; }
        public Guid CategoryNameId { get; set; }
        public Guid ImageId { get; set; }

        [ForeignKey(nameof(CategoryNameId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public required virtual Localization CategoryName { get; set; }

        [ForeignKey(nameof(ImageId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]

        public required virtual Image Image { get; set; }
    }
}
