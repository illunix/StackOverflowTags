namespace StackOverflowTags.Core.DTOs;

public sealed record TagDto(
    string Name,
    long UsageCount,
    double PopularityPercentage
);