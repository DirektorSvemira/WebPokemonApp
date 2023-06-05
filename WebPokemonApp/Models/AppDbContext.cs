using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WebPokemonApp.Models

{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Pokemon>().Property(f => f.Cijena).HasPrecision(10, 2);

            builder.Entity<Kategorija>().HasData(
                new Kategorija() { Id = 1, Naziv = "Fire (Vatra)" },
                new Kategorija() { Id = 2, Naziv = "Water (Voda)" },
                new Kategorija() { Id = 3, Naziv = "Electric (Električni)" },
                new Kategorija() { Id = 4, Naziv = "Ground (Zemljani)" },
                new Kategorija() { Id = 5, Naziv = "Flying (Leteci)" }
                );


            builder.Entity<Pokemon>().HasData(
               new Pokemon() { Id = 1, Naziv = "Charizard ", Cijena = 258.30m, DatumIzlaska = DateTime.Parse("1996-07-27"), SlikaUrl = "https://m.media-amazon.com/images/I/71nbfl-JklS.jpg", KategorijaId = 1 },
                new Pokemon() { Id = 2, Naziv = "Squirtle ", Cijena = 100.99m, DatumIzlaska = DateTime.Parse("1996-08-21"), SlikaUrl = "https://m.media-amazon.com/images/I/51TxlvrsoBL._AC_UF894,1000_QL80_.jpg", KategorijaId = 2 },
                new Pokemon() { Id = 3, Naziv = "Pikachu", Cijena = 5000.00m, DatumIzlaska = DateTime.Parse("1996-12-18"), SlikaUrl = "https://m.media-amazon.com/images/I/514L7XZClXL._AC_UF894,1000_QL80_.jpg", KategorijaId = 3 },
                new Pokemon() { Id = 4, Naziv = "Sandshrew", Cijena = 3500.00m, DatumIzlaska = DateTime.Parse("1999-07-18"), SlikaUrl = "https://m.media-amazon.com/images/I/51Zr7jOw7vL._AC_UF894,1000_QL80_.jpg", KategorijaId = 4 },
                new Pokemon() { Id = 5, Naziv = "Pidgey", Cijena = 2439.59m, DatumIzlaska = DateTime.Parse("1999-04-10"), SlikaUrl = "https://assets.pokemon.com/assets/cms2/img/cards/web/POP4/POP%20Series%204_Cards_EN_12.png", KategorijaId = 5 }


                );




        }
    }
}
