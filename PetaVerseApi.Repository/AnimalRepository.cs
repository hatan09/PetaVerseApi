using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.Repository.Extensions;
using System.Linq.Expressions;

namespace PetaVerseApi.Repository
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Animal?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
            => await FindAll(a => a.Id == id)
                    .Include(a => a.PetAvatar)
                    .Include(a => a.Breed)
                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<string> Generate6DigitCodeAsync()
        {
            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");
            if (code.Distinct().Count() == 1)
            {
                var isDuplicate = await _dbSet.AnyAsync(animal => animal.SixDigitCode == code);
                if(isDuplicate)
                   code = await Generate6DigitCodeAsync();
            }
            return code;
        }

        public async Task<Animal?> FindAnimalWithFullInfo(int id, CancellationToken cancellationToken)
        {
            var animal = await _dbSet.Include(a => a.Breed)
                                     .Include(a => a.PetAvatar)
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            return animal != null ? animal : null;
        }




         
        //public override IQueryable<Animal> FindAll(Expression<Func<Animal, bool>>? predicate = null)
        //    => _dbSet.WhereIf(predicate != null, predicate!)
        //             .Include(b => b.Species);
    }
}
