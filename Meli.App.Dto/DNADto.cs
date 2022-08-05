using System;

namespace Meli.App.Dto
{
    public class DNADto
    {
        public Guid Id { get; set; }
        public string Dna { get; set; }
        public bool? IsMutant { get; set; }
    }
}