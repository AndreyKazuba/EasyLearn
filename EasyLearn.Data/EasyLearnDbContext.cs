using Microsoft.EntityFrameworkCore;
using EasyLearn.Data.Models;

namespace EasyLearn.Data
{
    public class EasyLearnDbContext : DbContext
    {
        public EasyLearnDbContext() { }

        public EasyLearnDbContext(DbContextOptions<EasyLearnDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=EasyLearn2;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonRelation>().HasKey(relation => new { relation.EnglishWordId, relation.RussianWordId, relation.WordListId });
            modelBuilder.Entity<VerbPreposition>().HasKey(preposition => new { preposition.VerbId, preposition.PrepositionId, preposition.VerbPrepositionListId });
            modelBuilder.Entity<Example>().HasKey(example => new { example.RussianTranslationId, example.EnglishTranslationId });
        }

        public DbSet<EnglishUnit> EnglishUnits { get; set; }
        public DbSet<RussianUnit> RussianUnits { get; set; }

        public DbSet<Example> Examples { get; set; }

        public DbSet<CommonRelation> CommonRelations { get; set; }
        public DbSet<VerbPreposition> VerbPrepositions { get; set; }
        public DbSet<IrregularVerb> IrregularVerbs { get; set; }

        public DbSet<CommonWordList> CommonWordLists { get; set; }
        public DbSet<VerbPrepositionList> VerbPrepositionLists { get; set; }

        public DbSet<EasyLearnUser> Users { get; set; }
    }
}
