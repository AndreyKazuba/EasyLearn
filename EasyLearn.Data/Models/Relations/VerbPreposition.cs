using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLearn.Data.Models
{
    public class VerbPreposition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(VerbPrepositionList))]
        public int VerbPrepositionListId { get; set; }

        public VerbPrepositionList VerbPrepositionList { get; set; }

        [Required]
        [ForeignKey(nameof(Preposition))]
        public int PrepositionId { get; set; }

        public EnglishUnit Preposition { get; set; }

        [Required]
        [ForeignKey(nameof(Verb))]
        public int VerbId { get; set; }

        public EnglishUnit Verb { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        public string Comment { get; set; }

        public List<Example> Examples { get; set; } = new List<Example>();

        [Required]
        public DateTime CreationDateUtc { get; set; }
    }
}
