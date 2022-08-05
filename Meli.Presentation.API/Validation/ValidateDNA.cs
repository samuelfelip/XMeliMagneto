using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Meli.Presentation.API.Validation
{
    public class ValidateDNA : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string[] array = value as string[];
            var minLengthInArray = array.Min(x => x.Length);
            var maxLengthInArray = array.Max(x => x.Length);
            if (array == null || minLengthInArray != maxLengthInArray || array.Any(item => string.IsNullOrEmpty(item)) || array.Length != minLengthInArray) { return false; }
            var validCharsInDNA = @"[ATGC]$";
            Regex rg = new Regex(validCharsInDNA);
            var isOK = array.Any(v => rg.IsMatch(v));
            if (!isOK) { return false; }
            return true;
        }

    }
}
