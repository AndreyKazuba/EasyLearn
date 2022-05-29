using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class VerbPrepositionDictionnary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.DictionaryNameMinLength)]
        [MaxLength(ModelConstants.DictionaryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public EasyLearnUser User { get; set; }

        public List<VerbPreposition> VerbPrepositions { get; set; } = new List<VerbPreposition>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        public DateTime? ChangeDateUtc { get; set; }
    }
}
