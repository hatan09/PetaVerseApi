using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status?> FindByTitle(string title, CancellationToken cancellationToken =default);
        Task<Status?> FindByTopic(string Toppic, CancellationToken cancellationToken = default);
    }
}
