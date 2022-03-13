using System;
using System.ComponentModel.DataAnnotations;
using EasyLearn.Data.Enums;

namespace EasyLearn.Data.Models
{
    public class EnglishUnit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Value { get; set; }

        [Required]
        public UnitType Type { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }
    }
}
