using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Models.Spells
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpellType
    {
        Spell,
        Power,
        Cantrip
    }
}
