﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Domain.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RangeType
    {
        Touch = 1,
        Target = 2,
        Line = 4,
        Cone = 8,
        Burst = 16,
        Aura = 32,
        Self = 64
    }
}
