using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IBreedRepository : IBaseRepository<Breed>{
        Task<ICollection<Breed>> GetBySpeciesId(int speciesId, CancellationToken cancellationToken);
        IQueryable<Breed?> FindByIdIQueryable(int id, CancellationToken cancellationToken = default);
    }
}
