﻿namespace PetaVerseApi.Core.Entities
{
    public class Shedding : BaseEntity
    {
        public string        Info { get; set; } = String.Empty;
        public SheddingLevel Level { get; set; }
    }

    public enum SheddingLevel
    {
        Heavy, Medium, Minimal, None
    }
}