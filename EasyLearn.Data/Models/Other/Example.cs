using System;
using System.ComponentModel.DataAnnotations;
using EasyLearn.Data.Constants;

#pragma warning disable CS8618
namespace EasyLearn.Data.Models
{
    public class Example
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        [MinLength(ModelConstants.ExampleValueMinLength)]
        public string RussianValue { get; set; }

        [Required]
        [MaxLength(ModelConstants.ExampleValueMaxLength)]
        [MinLength(ModelConstants.ExampleValueMinLength)]
        public string EnglishValue { get; set; }

        [Required]
        public DateTime CreationDateUtc { get; set; }
    }
}
