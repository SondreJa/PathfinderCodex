using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Domain.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Castings
    {
        Somatic = 1,
        Verbal = 2,
        Material = 4
    }
}
