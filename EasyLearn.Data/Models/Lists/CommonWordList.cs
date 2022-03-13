using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyLearn.Data.Enums;

namespace EasyLearn.Data.Models
{
    public class CommonWordList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [MinLength(2)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public EasyLearnUser User { get; set; }

        public List<CommonRelation> Relations { get; set; } = new List<CommonRelation>();

        [Required]
        public WordListType Type { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }

        public DateTime? ChangeDateUtc { get; set; }
    }
}
