using System;
using System.ComponentModel.DataAnnotations;
using EasyLearn.Data.Enums;

#pragma warning disable CS8618

namespace EasyLearn.Data.Models
{
    public class EnglishUnit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.UnitValueMinLength)]
        [MaxLength(ModelConstants.UnitValueMaxLength)]
        public string Value { get; set; }

        [Required]
        public UnitType Type { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }
    }
}
