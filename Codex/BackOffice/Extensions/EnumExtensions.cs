using System;
using System.Collections.Generic;

namespace BackOffice.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetFlags<T>(this Enum input) where T : Enum
        {
            if (input == null)
                yield break;

            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return (T)value;
        }
    }
}
