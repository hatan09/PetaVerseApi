using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class SheddingRepository : BaseRepository<Shedding> , ISheddingRepository
    {
        public SheddingRepository(ApplicationDbContext context) : base(context) { }
    }
}
