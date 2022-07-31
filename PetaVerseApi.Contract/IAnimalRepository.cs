using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Contract
{
    public interface IAnimalRepository : IBaseRepository<Animal>
    {
        Task<string> Generate6DigitCodeAsync();
        Task<Animal?> FindAnimalWithFullInfo(int id, CancellationToken cancellationToken);
    }
}
