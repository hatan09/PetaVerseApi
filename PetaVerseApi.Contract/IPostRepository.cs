using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<Post?> FindByTitle(string title, CancellationToken cancellationToken =default);
        Task<Post?> FindByTopic(string Toppic, CancellationToken cancellationToken = default);
    }
}
