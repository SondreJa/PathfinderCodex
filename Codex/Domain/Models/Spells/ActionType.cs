using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Models.Spells
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActionType
    {
        Normal,
        Reaction,
        Free,
        Long
    }
}
