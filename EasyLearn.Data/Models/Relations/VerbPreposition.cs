using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class VerbPreposition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(VerbPrepositionDictionary))]
        public int VerbPrepositionDictionaryId { get; set; }

        public VerbPrepositionDictionnary VerbPrepositionDictionary { get; set; }

        [Required]
        [ForeignKey(nameof(Preposition))]
        public int PrepositionId { get; set; }

        public EnglishUnit Preposition { get; set; }

        [Required]
        [ForeignKey(nameof(Verb))]
        public int VerbId { get; set; }

        public EnglishUnit Verb { get; set; }

        [MinLength(ModelConstants.VerbPrepositionTranslationMinLength)]
        [MaxLength(ModelConstants.VerbPrepositionTranslationMaxLength)]
        public string Translation { get; set; }

        public List<Example> Examples { get; set; } = new List<Example>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        [MaxLength(ModelConstants.VerbPrepositionCommentMaxLength)]
        public string? Comment { get; set; }
    }
}
