using System.Data;

namespace PetaVerseApi.Interfaces
{
    public interface IExcelHandlerService
    {
        void CheckFileFormat(string filename);
        Task<DataRowCollection> GetRows(IFormFile file, CancellationToken cancellationToken);
    }
}
