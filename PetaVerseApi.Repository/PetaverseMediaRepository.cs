using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class PetaverseMediaRepository : BaseRepository<PetaverseMedia> , IPetaverseMediaRepository
    {
        public PetaverseMediaRepository(ApplicationDbContext context) : base(context) { }
    }
}
