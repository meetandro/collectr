namespace CollectR.Application.Common;

public static class EntityErrors
{
    public static Error NotFound(Guid id) =>
        new("Entity.NotFound", $"The entity with Id '{id}' was not found");

    public static Error OneOrMoreDoesntExist() =>
        new("Entity.OneOrMoreDoesntExist", $"The entity with Id was not found");
}
