using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IBreedRepository : IBaseRepository<Breed>{
        Task<ICollection<Breed>> GetBySpeciesId(int speciesId, CancellationToken cancellationToken);
    }
}
