using EasyLearn.Data.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace EasyLearn.Data.Models
{
    public class EasyLearnUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.UserNameMinLength)]
        [MaxLength(ModelConstants.UserNameMaxLength)]
        public string Name { get; set; }

        public List<CommonDictionary> CommonDictionaries { get; set; } = new List<CommonDictionary>();

        public List<VerbPrepositionDictionnary> VerbPrepositionDictionaries { get; set; } = new List<VerbPrepositionDictionnary>();

        public bool IsCurrent { get; set; }
    }
}
