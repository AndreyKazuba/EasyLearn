using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class CommonRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(CommonDictionary))]
        public int CommonDictionaryId { get; set; }

        public CommonDictionary CommonDictionary { get; set; }

        [Required]
        [ForeignKey(nameof(RussianUnit))]
        public int RussianUnitId { get; set; }

        public RussianUnit RussianUnit { get; set; }

        [Required]
        [ForeignKey(nameof(EnglishUnit))]
        public int EnglishUnitId { get; set; }

        public EnglishUnit EnglishUnit { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }

        [MaxLength(ModelConstants.CommonRelationCommentMaxLength)]
        public string? Comment { get; set; }
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
    }
}
