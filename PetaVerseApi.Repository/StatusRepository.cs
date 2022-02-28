using Microsoft.EntityFrameworkCore;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetaVerseApi.Repository
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context) { }

        public override IQueryable<Status> FindAll(Expression<Func<Status, bool>>? predicate = null)
            => _dbSet
                .WhereIf(predicate != null, predicate!)
                .Include(s => s.User);

        public async Task<Status?> FindByTitle(string title, CancellationToken cancellationToken = default)
            => await FindAll()
                .Where(s => s.Title == title)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Status?> FindByTopic(string toppic, CancellationToken cancellationToken = default)
            => await FindAll()
                .Where(s => s.Toppic == toppic)
                .FirstOrDefaultAsync(cancellationToken); 
    }
}
