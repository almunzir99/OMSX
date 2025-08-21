using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.VendorsService.Entities
{
    public class VendorCategory : AuditEntityBase
    {
        public Guid CategoryNameId { get; set; }
        public Guid DescriptionId { get; set; }

        [ForeignKey(nameof(CategoryNameId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public Localization? CategoryName { get; set; }

        [ForeignKey(nameof(DescriptionId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public Localization? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public VendorCategory? ParentCategory { get; set; }
    }
}
