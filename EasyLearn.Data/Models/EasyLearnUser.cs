using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLearn.Data.Models
{
    public class EasyLearnUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string NickName { get; set; }

        public List<CommonWordList> CommonWordLists { get; set; } = new List<CommonWordList>();

        public List<VerbPrepositionList> VerbPrepositionLists { get; set; } = new List<VerbPrepositionList>();

        public bool IsCurrent { get; set; }
    }
}
