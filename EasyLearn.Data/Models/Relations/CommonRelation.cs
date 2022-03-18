using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<Example> Examples { get; set; } = new List<Example>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        [MinLength(ModelConstants.RelationCommentMinLength)]
        [MaxLength(ModelConstants.RelationCommentMaxLength)]
        public string? Comment { get; set; }
    }
}
