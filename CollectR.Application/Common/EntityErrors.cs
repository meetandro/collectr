namespace CollectR.Application.Common;

public static class EntityErrors
{
    public static Error NotFound(Guid id) =>
        new("Entity.NotFound", $"The entity with Id '{id}' was not found");

    public static Error OneOrMoreDoesntExist() =>
        new("Entity.OneOrMoreDoesntExist", $"One or more entities requested not found.");

    public static Error HasAssignedEntities(string entity, Guid id) =>
        new(
            "Entity.HasAssignedEntities",
            $"Entity of type {entity} with Id {id} has entities assigned to it."
        );
}
