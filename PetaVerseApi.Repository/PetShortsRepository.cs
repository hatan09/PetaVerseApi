using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class PetShortsRepository : BaseRepository<PetShorts> , IPetShortsRepository
    {
        public PetShortsRepository(ApplicationDbContext context) : base(context) { }
    }
}
