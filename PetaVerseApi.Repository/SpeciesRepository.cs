using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;

namespace PetaVerseApi.Repository
{
    public class SpeciesRepository : BaseRepository<Species> , ISpeciesRepository
    {
        public SpeciesRepository(ApplicationDbContext context) : base(context) { }
    }
}
