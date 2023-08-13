using Marvel.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Database.Context
{
    public class MarvelContext : DbContext
    {
        public MarvelContext(DbContextOptions<MarvelContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterComic> CharacterComics { get; set; }
        public DbSet<Comic> Comics { get; set; }
        public DbSet<DownloadLog> DownloadLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("Character");
            modelBuilder.Entity<CharacterComic>().ToTable("CharacterComic");
            modelBuilder.Entity<Comic>().ToTable("Comic");
            modelBuilder.Entity<DownloadLog>().ToTable("DownloadLog");
        }
    }
}
