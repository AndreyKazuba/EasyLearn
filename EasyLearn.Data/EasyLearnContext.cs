using EasyLearn.Data.Models;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace EasyLearn.Data
{
    public class EasyLearnContext : DbContext
    {
        public EasyLearnContext() { }

        public EasyLearnContext(DbContextOptions<EasyLearnContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonRelation>().HasKey(commonRelation => new { commonRelation.EnglishUnitId, commonRelation.RussianUnitId, commonRelation.CommonDictionaryId });
            modelBuilder.Entity<VerbPreposition>().HasKey(verbPreposition => new { verbPreposition.VerbId, verbPreposition.PrepositionId, verbPreposition.VerbPrepositionDictionaryId });
            modelBuilder.Entity<Example>().HasKey(example => new { example.RussianTranslationId, example.EnglishTranslationId });
        }

        public DbSet<EnglishUnit> EnglishUnits { get; set; }
        public DbSet<RussianUnit> RussianUnits { get; set; }

        public DbSet<Example> Examples { get; set; }

        public DbSet<CommonRelation> CommonRelations { get; set; }
        public DbSet<VerbPreposition> VerbPrepositions { get; set; }
        public DbSet<IrregularVerb> IrregularVerbs { get; set; }

        public DbSet<CommonDictionary> CommonDictionaries { get; set; }
        public DbSet<VerbPrepositionDictionnary> VerbPrepositionDictionaries { get; set; }

        public DbSet<EasyLearnUser> Users { get; set; }
    }
}
