using Meli.App.Dto;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Meli.Presentation.API.Validation;

// <snippet_Class>
public class DNAAttribute : ValidationAttribute
{
    public DNAAttribute(string[] dna)
        => Dna = dna;
    public string[] Dna { get; set;}
    public string GetErrorMessage() =>
        $"La Secuencia de ADN no es correcta";

    //protected override ValidationResult IsValid(object value)
    //{
    //    string[] array = value as string[];
    //    var minLengthInArray = array.Min(x => x.Length);
    //    if (array == null || array.Any(item => string.IsNullOrEmpty(item)) || array.Length == minLengthInArray)
    //    {
            
    //        return new ValidationResult(this.ErrorMessage);
    //    }
    //    var validCharsInDNA = @"^[A-Z]$";
    //    Regex rg = new Regex(validCharsInDNA);
    //    var isOK = array.Any(v => rg.IsMatch(v));
    //    if (!isOK)
    //    {
    //        return new ValidationResult(this.ErrorMessage);
    //    }

    //    return ValidationResult.Success;
        
    //}
}
// </snippet_Class>
