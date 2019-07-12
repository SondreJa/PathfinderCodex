using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BackOffice.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SaveTarget
    {
        AC = 1,
        Fortitude = 2,
        Reflex = 4,
        Will = 8
    }
}