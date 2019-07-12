using BackOffice.Models;
using Domain.Models.Spells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BODurationType = BackOffice.Models.Spells.DurationType;

namespace BackOffice.Controllers
{
    public class SpellController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string spellUri;
        private static readonly Regex whitespaceReplacer = new Regex(@"\s+");

        public SpellController(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            spellUri = configuration.GetSection("ApiUrl").Value;
        }

        // GET: Spell
        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync($"{spellUri}/api/Spells/GetAll");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var spells = JsonConvert.DeserializeObject <IEnumerable<Spell>>(jsonResponse);
            var viewModelSpells = spells.Select(spell => MapSpell(spell));
            return View(viewModelSpells);
        }

        // GET: Spell/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Spell/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spell/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] SpellViewModel spell)
        {
            try
            {
                var domainSpell = MapSpell(spell);
                var jsonSpell = JsonConvert.SerializeObject(domainSpell);
                var content = new StringContent(jsonSpell, Encoding.UTF8, "application/json");
                await httpClient.PostAsync($"{spellUri}/api/Spells", content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Spell/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Spell/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Spell/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Spell/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Spell MapSpell(SpellViewModel spell)
        {
            var jsonViewModel = JsonConvert.SerializeObject(spell);
            var domainSpell = JsonConvert.DeserializeObject<Spell>(jsonViewModel);

            if (domainSpell.Type == null || domainSpell.Type == SpellType.Cantrip)
                domainSpell.Level = 0;

            var viewModelTraits = domainSpell.Traits.FirstOrDefault();
            viewModelTraits = ReplaceWhitespace(viewModelTraits, "");
            domainSpell.Traits = viewModelTraits.Split(",");

            var castingDuration = new CastingDuration
            {
                Interval = spell?.CastingInterval,
                DurationType = (Domain.Models.Spells.DurationType?)spell.CastingDurationType
            };

            var duration = new Duration
            {
                Interval = spell.DurationInterval,
                DurationType = (DurationType?)spell.DurationType,
                IsDismissable = spell.IsDismissable,
                IsConcentration = spell.IsConcentration
            };

            domainSpell.CastingDuration = castingDuration;
            domainSpell.Duration = duration;
            return domainSpell;
        }

        private SpellViewModel MapSpell(Spell spell)
        {
            if (spell == null)
                return null;

            var jsonSpell = JsonConvert.SerializeObject(spell);
            var viewModelSpell = JsonConvert.DeserializeObject<SpellViewModel>(jsonSpell);

            viewModelSpell.CastingInterval = spell.CastingDuration?.Interval;
            viewModelSpell.CastingDurationType = (BODurationType?)spell.CastingDuration?.DurationType;

            viewModelSpell.DurationInterval = spell.Duration?.Interval;
            viewModelSpell.DurationType = (BODurationType?)spell.Duration?.DurationType;
            viewModelSpell.IsDismissable = spell.Duration?.IsDismissable;
            viewModelSpell.IsConcentration = spell.Duration?.IsConcentration;

            return viewModelSpell;
        }

        private static string ReplaceWhitespace(string input, string replacement)
        {
            if (input == null)
                return null;
            return whitespaceReplacer.Replace(input, replacement);
        }
    }
}