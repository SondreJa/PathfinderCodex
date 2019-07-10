using Domain.Models.Spells;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface ISpellHandler
    {
        Task AddSpell(Spell spell);
        Task RemoveSpell(string name);
        Task<Spell> GetSpell(string name);
        Task<IEnumerable<Spell>> GetAllSpells();
    }
}
