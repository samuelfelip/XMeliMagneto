using Meli.Presentation.API.Validation;

namespace Meli.Presentation.API.Model
{
    public class DNAReq
    {
        [ValidateDNA]
        public string[] Dna { get; set; }

    }
}
