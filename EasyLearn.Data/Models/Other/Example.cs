using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace EasyLearn.Data.Models
{
    public class Example
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(RussianTranslation))]
        public int RussianTranslationId { get; set; }

        public RussianUnit RussianTranslation { get; set; }

        [Required]
        [ForeignKey(nameof(EnglishTranslation))]
        public int EnglishTranslationId { get; set; }

        public EnglishUnit EnglishTranslation { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }
    }
}
