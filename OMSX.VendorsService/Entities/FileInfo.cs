using OMSX.Shared.Entities.Common;

namespace OMSX.VendorsService.Entities
{
    public class FileInfo : EntityBase
    {
        public required string Path { get; set; }
        public bool IsFullPath { get; set; }
    }
}
