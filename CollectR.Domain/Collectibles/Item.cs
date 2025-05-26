using CollectR.Domain.Enums;

namespace CollectR.Domain.Collectibles;

public class Item : Collectible
{
    public Color? Color { get; set; }

    public Condition? Condition { get; set; }
}
