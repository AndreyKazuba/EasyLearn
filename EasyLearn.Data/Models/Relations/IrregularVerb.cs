using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLearn.Data.Models
{
    public class IrregularVerb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(RussianWord))]
        public int RussianWordId { get; set; }

        public RussianUnit RussianWord { get; set; }

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

        public List<Example> Examples { get; set; } = new List<Example>();

        [MinLength(2)]
        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
