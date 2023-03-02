using System.Text.Json.Serialization;

namespace StackOverflowTags.Core.DTOs;

public sealed record StackOverflowTagDto(
    [property: JsonPropertyName("has_synonyms")] bool HasSynonyms,
    [property: JsonPropertyName("is_moderator_only")] bool IsModeratorOnly,
    [property: JsonPropertyName("is_required")] bool IsRequired,
    [property: JsonPropertyName("count")] long Count,
    [property: JsonPropertyName("name")] string Name
);

