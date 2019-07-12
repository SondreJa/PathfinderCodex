using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BackOffice.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Categories
    {
        Attack = 1,
        Restoration = 2,
        Buff = 4,
        Debuff = 8
    }
}