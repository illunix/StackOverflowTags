using System.Text.Json.Serialization;

namespace StackOverflowTags.Core.DTOs;

public sealed record GetStackOverflowTagsResponseDto(
    [property: JsonPropertyName("items")] IReadOnlyList<StackOverflowTagDto> Items,
    [property: JsonPropertyName("has_more")] bool HasMore,
    [property: JsonPropertyName("quota_max")] int QuotaMax,
    [property: JsonPropertyName("quota_remaining")] int QuotaRemaining
);

[JsonSerializable(typeof(GetStackOverflowTagsResponseDto))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]
public sealed partial class GetStackOverflowTagsResponseDtoJsonContext : JsonSerializerContext { }