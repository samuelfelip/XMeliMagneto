using Meli.Data;
using Meli.Data.Domain.Entities;

namespace Meli.Data.Domain
{
    public interface IDNARepository : IRepository<DNAEntity, Guid>
    {
    }
}