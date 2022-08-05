using System;
using System.ComponentModel.DataAnnotations;

namespace Meli.Data.Domain.Entities
{
    public class DNAEntity : Entity<Guid>
    {
        [Required]
        public string Dna { get; set; }
        public bool? IsMutant { get; set; }
    }
}