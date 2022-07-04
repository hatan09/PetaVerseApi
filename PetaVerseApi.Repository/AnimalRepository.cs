using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetaVerseApi.Repository
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Animal?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    => await FindAll(b => b.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        public override IQueryable<Animal> FindAll(Expression<Func<Animal, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!)
                     .Include(b => b.Species);
    }
}
