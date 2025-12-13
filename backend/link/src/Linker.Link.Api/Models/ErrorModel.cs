using System.Text.Json.Serialization;

namespace Linker.Link.Api.Models;

internal record ErrorModel(
    [property: JsonPropertyName("errors")]
    IEnumerable<string> Errors,

    [property: JsonPropertyName("resultType")]
    int ResultType
)
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess => false;
}
