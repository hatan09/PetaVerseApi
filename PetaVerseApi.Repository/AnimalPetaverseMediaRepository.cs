using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{

    public class AnimalPetaverseMediaRepository : BaseRepository<AnimalPetaverseMedia>, IAnimalPetaverseMediaRepository
    {
        public AnimalPetaverseMediaRepository(ApplicationDbContext context) : base(context) { }
    }
}
