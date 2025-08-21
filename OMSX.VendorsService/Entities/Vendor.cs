using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Entities.Common;
using OMSX.VendorsService.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSX.VendorsService.Entities
{
    public class Vendor : AuditEntityBase
    {

        public Guid BusinessNameId { get; set; }
        public Guid DescriptionId { get; set; }
        public required string LegalName { get; set; }
        public required string Website { get; set; }
        public string? TaxId { get; set; }
        public string? BusinessLicense { get; set; }
        public string? VATNumber { get; set; }
        public BusinessType BusinessType { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhone { get; set; }
        public string? SupportEmail { get; set; }
        public string? SupportPhone { get; set; }
        public Guid BusinessAddressId { get; set; }
        public Guid LogoId { get; set; }
        public Guid VendorCategoryId { get; set; }

        [ForeignKey(nameof(BusinessNameId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public required Localization BusinessName { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public required Localization Description { get; set; }
        public VendorCategory? VendorCategory { get; set; }
        public BusinessAddress? BusinessAddress { get; set; }
        public FileInfo? Logo { get; set; }



    }
}
