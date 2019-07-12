using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Models.Spells
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare
    }
}