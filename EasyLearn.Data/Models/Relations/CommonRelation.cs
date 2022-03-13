using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLearn.Data.Models
{
    public class CommonRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(WordList))]
        public int WordListId { get; set; }

        public CommonWordList WordList { get; set; }

        [Required]
        [ForeignKey(nameof(RussianWord))]
        public int RussianWordId { get; set; }

        public RussianUnit RussianWord { get; set; }

        [Required]
        [ForeignKey(nameof(EnglishWord))]
        public int EnglishWordId { get; set; }

        public EnglishUnit EnglishWord { get; set; }

        public List<Example> Examples { get; set; } = new List<Example>();

        [Required]
        public DateTime CreationDateUtc { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
