using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackOffice.Models.Spells
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DurationType
    {
        Unlimited,
        Varies,
        Round,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Year
    }
}
