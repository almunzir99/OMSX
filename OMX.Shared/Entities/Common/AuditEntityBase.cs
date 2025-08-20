namespace OMSX.Shared.Entities.Common
{
    public class AuditEntityBase : EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
