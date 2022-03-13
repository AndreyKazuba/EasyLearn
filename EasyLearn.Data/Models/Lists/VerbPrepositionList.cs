using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLearn.Data.Models
{
    public class VerbPrepositionList
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

        public List<VerbPreposition> VerbPrepositions { get; set; } = new List<VerbPreposition>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        public DateTime? ChangeDateUtc { get; set; }
    }
}
