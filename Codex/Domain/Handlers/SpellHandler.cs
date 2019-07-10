using Domain.Models.Spells;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class SpellHandler : ISpellHandler
    {
        private readonly ICosmosRepository<Spell> repository;

        public SpellHandler(ICosmosRepository<Spell> repository)
        {
            this.repository = repository;
        }

        public async Task AddSpell(Spell spell)
        {
            var now = DateTime.UtcNow;
            var existingEntity = await repository.Get(spell.Name);
            if (existingEntity != null && existingEntity.Timestamp >= now)
                return;

            var newEntity = new DocumentWrapper<Spell>
            {
                Id = spell.Name,
                Timestamp = now,
                Entity = spell
            };
            await repository.Upsert(newEntity);
        }

        public async Task<IEnumerable<Spell>> GetAllSpells()
        {
            var entities = await repository.GetMany(_ => true);
            var spells = entities.Select(e => e.Entity);
            return spells;
        }

        public async Task<Spell> GetSpell(string name)
        {
            var entity = await repository.Get(name);
            return entity?.Entity;
        }

        public async Task RemoveSpell(string name)
        {
            await repository.Delete(name);
        }
    }
}
