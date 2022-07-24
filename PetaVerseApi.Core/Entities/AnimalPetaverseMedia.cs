namespace PetaVerseApi.Core.Entities
{
    public class AnimalPetaverseMedia : BaseEntity
    {
        public int AnimalId                     { get; set; }
        public int PetaverMediaId               { get; set; }

        public Animal? Animal                   { get; set; }
        public PetaverseMedia? PetaverseMedia   { get; set; }
    }
}
