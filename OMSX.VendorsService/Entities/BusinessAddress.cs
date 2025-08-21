using OMSX.Shared.Entities.Common;

namespace OMSX.VendorsService.Entities
{
    public class BusinessAddress : EntityBase
    {
        public string? Street { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsPrimary { get; set; }
    }
}
