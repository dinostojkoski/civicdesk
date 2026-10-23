namespace CivicDesk.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected void MarkUpdated()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}