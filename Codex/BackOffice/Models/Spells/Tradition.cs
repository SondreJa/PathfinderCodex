using BackOffice.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BackOffice.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Tradition
    {
        Arcane = 1,
        Divine = 2,
        Occult = 4,
        Primal = 8
    }
}