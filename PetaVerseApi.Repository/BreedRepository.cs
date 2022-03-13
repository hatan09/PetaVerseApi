using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class BreedRepository : BaseRepository<Breed> , IBreedRepository
    {
        public BreedRepository(ApplicationDbContext context) : base(context) { }
    }
}
