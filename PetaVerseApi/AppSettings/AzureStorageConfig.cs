namespace PetaVerseApi.AppSettings
{
    public class AzureStorageConfig
    {
        public string   AccountName         { get; set; } = String.Empty;
        public string   AccountKey          { get; set; } = String.Empty;
        public string   ImageContainer      { get; set; } = String.Empty;
        public string   ThumbnailContainer  { get; set; } = String.Empty;
        public string   BlobContainerUrl    { get; set; } = String.Empty;
    }
}
