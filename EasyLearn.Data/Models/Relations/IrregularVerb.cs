using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class IrregularVerb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(RussianUnit))]
        public int RussianUnitId { get; set; }

        public RussianUnit RussianUnit { get; set; }

        [Required]
        [ForeignKey(nameof(FirstForm))]
        public int FirstFormId { get; set; }

        public EnglishUnit FirstForm { get; set; }

        [Required]
        [ForeignKey(nameof(SecondForm))]
        public int SecondFormId { get; set; }

        public EnglishUnit SecondForm { get; set; }

        [Required]
        [ForeignKey(nameof(ThirdForm))]
        public int ThirdFormId { get; set; }

        public EnglishUnit ThirdForm { get; set; }

        [MaxLength(ModelConstants.IrregularVerbCommentMaxLength)]
        public string? Comment { get; set; }

        [Range(ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue)]
        public int Rating { get; set; }
    }
}
