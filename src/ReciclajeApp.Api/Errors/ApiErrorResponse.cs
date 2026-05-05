using System.Text.Json.Serialization;

namespace ReciclajeApp.Api.Errors;

public sealed class ApiErrorResponse
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("errors")]
    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();

    [JsonPropertyName("traceId")]
    public string? TraceId { get; init; }
}
