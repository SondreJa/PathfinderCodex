using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackOffice.Models.Spells
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpellType
    {
        Spell,
        Power,
        Cantrip
    }
}
