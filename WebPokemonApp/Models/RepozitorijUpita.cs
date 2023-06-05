using Microsoft.EntityFrameworkCore;
using WebPokemonApp.Models;

namespace WebPokemonApp.Models
{
    public class RepozitorijUpita : IRepozitorijUpita
    {
        private readonly AppDbContext _appDbContext;
        public RepozitorijUpita(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Pokemon pokemon)
        {
            _appDbContext.Add(pokemon);
            _appDbContext.SaveChanges();
        }
        public void Create(Kategorija kategorija)
        {
            _appDbContext.Add(kategorija);
            _appDbContext.SaveChanges();
        }
        public void Delete(Pokemon pokemon)
        {
            _appDbContext.Pokemon.Remove(pokemon);
            _appDbContext.SaveChanges();
        }
        public void Delete(Kategorija kategorija)
        {
            _appDbContext.Kategorija.Remove(kategorija);
            _appDbContext.SaveChanges();
        }
        public Pokemon DohvatiPokemonaSIdom(int id)
        {
            return _appDbContext.Pokemon
                .Include(k => k.Kategorija)
                .FirstOrDefault(f => f.Id == id);
        }
        public Kategorija DohvatiKategorijuSIdom(int id)
        {
            return _appDbContext.Kategorija.Find(id);
        }

        public int KategorijaSljedeciId()
        {
            int zadnjiId = _appDbContext.Kategorija
               .Count();

            int sljedeciId = zadnjiId + 1;
            return sljedeciId;
        }

        public IEnumerable<Pokemon> PopisPokemona()
        {

            return _appDbContext.Pokemon.Include(k => k.Kategorija);
        }



        public IEnumerable<Kategorija> PopisKategorija()
        {
            return _appDbContext.Kategorija;
        }

        public int SljedeciId()
        {
            int zadnjiId = _appDbContext.Pokemon
                .Include(k => k.Kategorija)
                .Max(x => x.Id);

            int sljedeciId = zadnjiId + 1;
            return sljedeciId;
        }

        public void Update(Pokemon pokemon)
        {
            _appDbContext.Pokemon.Update(pokemon);
            _appDbContext.SaveChanges();
        }

        public void Update(Kategorija kategorija)
        {
            _appDbContext.Kategorija.Update(kategorija);
            _appDbContext.SaveChanges();
        }
    }
}
