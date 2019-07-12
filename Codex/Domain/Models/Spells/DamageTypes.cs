using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Domain.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DamageTypes
    {
        Bludgeoning = 1,
        Piercing = 2,
        Slashing = 4,
        Acid = 8,
        Cold = 16,
        Electricity = 32,
        Fire = 64,
        Sonic = 128,
        Force = 256,
        Negative = 512,
        Positive = 1024,
        Mental = 2048,
        Chaotic = 4096,
        Evil = 8192,
        Good = 16384,
        Lawful = 32768
    }
}