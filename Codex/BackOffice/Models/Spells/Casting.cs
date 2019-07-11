using BackOffice.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BackOffice.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
    public enum Castings
    {
        Somatic = 1,
        Verbal = 2,
        Material = 4
    }
}
