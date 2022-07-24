using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class UserAnimalRepository : BaseRepository<UserAnimal> , IUserAnimalRepository
    {
        public UserAnimalRepository(ApplicationDbContext context) : base(context) { }
    }
}
