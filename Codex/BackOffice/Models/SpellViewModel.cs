using BackOffice.Extensions;
using BackOffice.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackOffice.Models
{
    public class SpellViewModel
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public Rarity? Rarity { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public Tradition? Tradition { get; set; }
        public SpellType? Type { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public Categories? Categories { get; set; }
        public ActionType? Action { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public Castings? Castings { get; set; }

        // Casting duration
        public int? CastingInterval { get; set; }
        public DurationType? CastingDurationType { get; set; }
        public int? Range { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public RangeType? Area { get; set; }
        public string Targets { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public SaveTarget? SaveTarget { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public DamageTypes? DamageTypes { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public Conditions? CauseConditions { get; set; }

        [ModelBinder(BinderType = typeof(EnumFlagsModelBinder))]
        public Conditions? CureConditions { get; set; }

        public string Trigger { get; set; }
        
        // Duration
        public int? DurationInterval { get; set; }
        public DurationType? DurationType { get; set; }
        public bool? IsDismissable { get; set; }
        public bool? IsConcentration { get; set; }

        public string Requirements { get; set; }
        public IEnumerable<string> Traits { get; set; }
        public bool? IsHeightenable { get; set; }
        public string Description { get; set; }
    }
}
