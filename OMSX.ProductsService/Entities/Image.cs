using OMSX.Shared.Entities.Common;

namespace OMSX.ProductsService.Entities
{
    public class Image : EntityBase
    {
        public required string Path { get; set; }
        public bool IsFullPath { get; set; }
    }
}
