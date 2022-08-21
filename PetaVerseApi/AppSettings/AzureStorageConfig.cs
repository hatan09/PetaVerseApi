namespace PetaVerseApi.AppSettings
{
    public class AzureStorageConfig
    {
        public string   AccountName          { get; set; } = String.Empty;
        public string   AccountKey           { get; set; } = String.Empty;
        public string   PetaverseGallery     { get; set; } = String.Empty;
        public string   PetaverseAvatar      { get; set; } = String.Empty;
        public string   ThumbnailContainer   { get; set; } = String.Empty;
        public string   BlobContainerUrl     { get; set; } = String.Empty;
        public string   BlobConnectionString { get; set; } = String.Empty;
    }
}
