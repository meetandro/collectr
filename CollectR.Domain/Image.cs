using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Image : BaseEntity
{
    public string Uri { get; set; } = string.Empty;

    public string? Alt { get; set; }

    public int CollectibleId { get; set; }
}
