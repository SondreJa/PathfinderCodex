using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackOffice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BackOffice.Controllers
{
    public class SpellController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string spellUri;

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
            var spells = JsonConvert.DeserializeObject <IEnumerable<SpellViewModel>>(jsonResponse);
            return View(spells);
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
                var jsonSpell = JsonConvert.SerializeObject(spell);
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
    }
}