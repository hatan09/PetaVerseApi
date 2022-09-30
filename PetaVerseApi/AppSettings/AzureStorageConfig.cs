﻿namespace PetaVerseApi.AppSettings
{
    public class AzureStorageConfig
    {
        public string   AccountName          { get; set; } = String.Empty;
        public string   AccountKey           { get; set; } = String.Empty;
        public string   PetaverseGeneralFile { get; set; } = String.Empty;
        public string   PetaversePhotos      { get; set; } = String.Empty;
        public string   PetaversePetAvatars  { get; set; } = String.Empty;
        public string   ThumbnailContainer   { get; set; } = String.Empty;
        public string   BlobContainerUrl     { get; set; } = String.Empty;
        public string   BlobConnectionString { get; set; } = String.Empty;
    }
}
