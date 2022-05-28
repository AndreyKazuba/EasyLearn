using System;
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

        [Required]
        public DateTime CreationDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }

        public bool IsFirstExampleExist => !string.IsNullOrEmpty(this.FirstExampleRussianValue) && !string.IsNullOrEmpty(this.FirstExampleEnglishValue);

        [MinLength(ModelConstants.ExampleValueMinLength)]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        public string? FirstExampleRussianValue { get; set; }

        [MinLength(ModelConstants.ExampleValueMinLength)]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        public string? FirstExampleEnglishValue { get; set; }

        public bool IsSecondExampleExist => !string.IsNullOrEmpty(this.SecondExampleRussianValue) && !string.IsNullOrEmpty(this.SecondExampleEnglishValue);

        [MinLength(ModelConstants.ExampleValueMinLength)]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        public string? SecondExampleRussianValue { get; set; }

        [MinLength(ModelConstants.ExampleValueMinLength)]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        public string? SecondExampleEnglishValue { get; set; }

        [Range(ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue)]
        public int Rating { get; set; }

        [Range(ModelConstants.CorrectAnswersStreakMinValue, ModelConstants.CorrectAnswersStreakMaxValue)]
        public int CorrectAnswersStreak { get; set; }

        public bool Studied { get; set; }

        public int Priority => Rating / 10;
    }
}
