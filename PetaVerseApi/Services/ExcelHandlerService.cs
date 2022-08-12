using ExcelDataReader;
using PetaVerseApi.Interfaces;
using System.Data;

namespace PetaVerseApi.Services
{
    public class ExcelHandlerService : IExcelHandlerService
    {
        public ExcelHandlerService()
        {
        }

        private static void CheckFileFormat(string filename)
        {
            if (!Path.GetExtension(filename).Equals(".xlsx"))
                throw new ("File should be compressed in '.xlsx' format");
        }

        public async Task<DataRowCollection> GetRows(IFormFile file, CancellationToken cancellationToken)
        {
            CheckFileFormat(file.FileName);

            var tempPath = Path.GetTempFileName();

            using (FileStream stream = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.RandomAccess | FileOptions.DeleteOnClose))
            {
                await file.CopyToAsync(stream, cancellationToken);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    var dataSet = reader.AsDataSet(conf);

                    var dataTable = dataSet.Tables[0];

                    return dataTable.Rows;
                }
            }
        }
    }
}
