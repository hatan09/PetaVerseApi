using System.Data;

namespace PetaVerseApi.Interfaces
{
    public interface IExcelHandlerService
    {
        Task<DataRowCollection> GetRows(IFormFile file, CancellationToken cancellationToken);
    }
}
