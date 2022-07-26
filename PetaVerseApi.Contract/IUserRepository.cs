using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default);
    }
}
