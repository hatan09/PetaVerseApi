using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.Repository.Extensions;
using System.Linq.Expressions;

namespace PetaVerseApi.Repository
{
    public class BreedRepository : BaseRepository<Breed> , IBreedRepository
    {
        public BreedRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Breed?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
            => await FindAll(b => b.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);
        public override IQueryable<Breed> FindAll(Expression<Func<Breed, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!)
                     .Include(b => b.Species);
    }
}
