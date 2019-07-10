using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Models.Spells
{
    public class Spell
    {
        public string Name { get; set; }
        public int? Level { get; set; }
        public SpellType? Type { get; set; }
        public ActionType? Action { get; set; }
        public Castings? Castings { get; set; }
        public CastingDuration CastingDuration { get; set; }
        public int? Range { get; set; }
        public RangeType? Area { get; set; }
        public string Targets { get; set; }
        public string Trigger { get; set; }
        public Duration Duration { get; set; }
        public string Requirements { get; set; }
        public IEnumerable<string> Traits { get; set; }
        public bool? IsHeightenable { get; set; }
        public string Description { get; set; }
    }
}
