using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class CommonDictionary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.DictionaryNameMinLength)]
        [MaxLength(ModelConstants.DictionaryNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModelConstants.DictionaryDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public EasyLearnUser User { get; set; }

        public List<CommonRelation> Relations { get; set; } = new List<CommonRelation>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        public DateTime? ChangeDateUtc { get; set; }
    }
}
