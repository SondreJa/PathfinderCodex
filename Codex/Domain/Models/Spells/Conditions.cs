using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Domain.Models.Spells
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Conditions : long
    {
        Accelerated = 1,
        Asleep = 2,
        Blinded = 4,
        Broken = 8,
        Concealed = 16,
        Confused = 32,
        Dazzled = 64,
        Dead = 128,
        Deafened = 256,
        Drained = 512,
        Dying = 1024,
        Encumbered = 2048,
        Enervated = 4096,
        Enfeebled = 8192,
        Entangled = 16384,
        Fascinated = 32768,
        Fatigued = 65536,
        FlatFooted = 131072,
        Fleeing = 262144,
        Friendly = 524288,
        Frightened = 1048576,
        Grabbed = 2097152,
        Hampered = 4194304,
        Helpful = 8388608,
        Hostile = 16777216,
        Immobile = 33554432,
        Indifferent = 67108864,
        Paralyzed = 134217728,
        PersistentDamage = 268435456,
        Petrified = 536870912,
        Prone = 1073741824,
        Quick = 2147483648,
        Restrained = 4294967296,
        Sense = 8589934592,
        Sick = 17179869184,
        Slowed = 34359738368,
        Sluggish = 68719476736,
        Stunned = 137438953472,
        Stupefied = 274877906944,
        Unconscious = 549755813888,
        Unfriendly = 1099511627776,
        Unseen = 2199023255552
    }
}