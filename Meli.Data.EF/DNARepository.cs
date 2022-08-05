using Meli.Data.Domain;
using Meli.Data.Domain.Entities;

namespace Meli.Data.EF
{
    public class DNARepository : Repository<DNAEntity, Guid>, IDNARepository
    {
        public DNARepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}