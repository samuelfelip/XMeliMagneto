using Meli.App.Dto;
using Meli.App.Services.Model;
using Meli.Data.Domain.Entities;
namespace Meli.Application.Services
{
    public interface IDNAService
    {
        Task AddDNA(DNAEntity dna);
        Task<IEnumerable<DNADto>> GetDNAs();
        Task<Stats> GetStats();
        Task<bool> FindMutant(string[] Dna);
    }
}