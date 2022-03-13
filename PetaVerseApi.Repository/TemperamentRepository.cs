using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaVerseApi.Repository
{
    public class TemperamentRepository : BaseRepository<Temperament> , ITemperamentRepository
    {
        public TemperamentRepository(ApplicationDbContext context) : base(context) { }
    }
}
