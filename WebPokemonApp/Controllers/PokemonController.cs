using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPokemonApp.Models;

namespace WebPokemonApp.Controllers
{

    public class PokemonController : Controller
    {
        private readonly IRepozitorijUpita _repozitorijUpita;
        public PokemonController(IRepozitorijUpita repozitorijUpita)
        {
            _repozitorijUpita = repozitorijUpita;
        }
        public IActionResult Index()
        {
            return View(_repozitorijUpita.PopisPokemona());
        }
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv");
            int sljedeciId = _repozitorijUpita.SljedeciId();
            Pokemon pokemon = new Pokemon() { Id = sljedeciId };
            return View(pokemon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Naziv,DatumIzlaska,Cijena,SlikaUrl,KategorijaId")] Pokemon pokemon)
        {
            ModelState.Remove("Kategorija");

            if (ModelState.IsValid)
            {
                _repozitorijUpita.Create(pokemon);
                return RedirectToAction("Index");
            }

            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", pokemon.KategorijaId);
            return View(pokemon);

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            Pokemon pokemon = _repozitorijUpita.DohvatiPokemonaSIdom(id);

            if (pokemon == null) { return NotFound(); }

            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", pokemon.KategorijaId);
            return View(pokemon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id,Naziv,DatumIzlaska,Cijena,SlikaUrl,KategorijaId")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Kategorija");

            if (ModelState.IsValid)
            {
                _repozitorijUpita.Update(pokemon);
                return RedirectToAction("Index");
            }

            ViewData["KategorijaId"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Id", "Naziv", pokemon.KategorijaId);
            return View(pokemon);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var pokemon = _repozitorijUpita.DohvatiPokemonaSIdom(Convert.ToInt16(id));

            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pokemon = _repozitorijUpita.DohvatiPokemonaSIdom(id);
            _repozitorijUpita.Delete(pokemon);
            return RedirectToAction("Index");

        }


        public ActionResult SearchIndex(string pokemonZanr, string searchString)
        {
            var zanr = new List<string>(); 

            var zanrUpit = _repozitorijUpita.PopisKategorija();

            ViewData["pokemonZanr"] = new SelectList(_repozitorijUpita.PopisKategorija(), "Naziv", "Naziv", zanrUpit);

            var pokemonii = _repozitorijUpita.PopisPokemona();

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                pokemonii = pokemonii.Where(s => s.Naziv.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (string.IsNullOrWhiteSpace(pokemonZanr))
                return View(pokemonii);
            else
            {
                return View(pokemonii.Where(x => x.Kategorija.Naziv == pokemonZanr));
            }

        }
    }
}

