using OMSX.Shared.Enums;

namespace OMSX.Shared.Entities.Common
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public StatusEnum Status { get; set; } = StatusEnum.Active;
    }
}
