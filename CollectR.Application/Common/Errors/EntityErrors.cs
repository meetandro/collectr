namespace CollectR.Application.Common.Errors;

public static class EntityErrors
{
    public static Error NotFound(Guid id) =>
        new("Entity.NotFound", $"The entity with Id '{id}' was not found");

    public static Error OneOrMoreDoesNotExist() =>
        new("Entity.OneOrMoreDoesNotExist", $"One or more entities requested not found.");

    public static Error HasAssignedEntities(string entityType, Guid id) =>
        new(
            "Entity.HasAssignedEntities",
            $"Entity of type {entityType} with Id {id} has entities assigned to it."
        );
}
