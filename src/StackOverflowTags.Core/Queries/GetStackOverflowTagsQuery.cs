using Mediator;
using StackOverflowTags.Core.DTOs;

namespace StackOverflowTags.Core.Queries;

public sealed record GetStackOverflowTagsQuery : IQuery<IReadOnlyList<TagDto>>;