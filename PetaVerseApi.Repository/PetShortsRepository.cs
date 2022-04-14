using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.Repository.Extensions;
using System.Linq.Expressions;

namespace PetaVerseApi.Repository
{
    public class PetShortsRepository : BaseRepository<PetShorts> , IPetShortsRepository
    {
        public PetShortsRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<PetShorts?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
            => await FindAll(b => b.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);
        public override IQueryable<PetShorts> FindAll(Expression<Func<PetShorts, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!)
                     .Include(p => p.Publisher)
                     .Include(p => p.Media)
                     .Include(p => p.Pet);
    }
}
