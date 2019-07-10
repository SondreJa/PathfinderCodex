using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Models.Spells
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
