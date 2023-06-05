using WebPokemonApp.Models;

namespace WebPokemonApp.Models
{
    public interface IRepozitorijUpita
    {

        IEnumerable<Pokemon> PopisPokemona();
        void Create(Pokemon pokemon);
        void Delete(Pokemon pokemon);
        void Update(Pokemon pokemon);
        int SljedeciId();
        int KategorijaSljedeciId();
        Pokemon DohvatiPokemonaSIdom(int id);

        IEnumerable<Kategorija> PopisKategorija();
        void Create(Kategorija kategorija);
        void Delete(Kategorija kategorija);
        void Update(Kategorija kategorija);

        Kategorija DohvatiKategorijuSIdom(int id);


    }
}

