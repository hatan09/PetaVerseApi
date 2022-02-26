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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }

        public override IQueryable<Post> FindAll(Expression<Func<Post, bool>>? predicate = null)
            => _dbSet
                .WhereIf(predicate != null, predicate!)
                .Include(p => p.User);

        public async Task<Post?> FindByTitle(string title, CancellationToken cancellationToken = default)
            => await FindAll()
                .Where(p => p.Title == title)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Post?> FindByTopic(string toppic, CancellationToken cancellationToken = default)
            => await FindAll()
                .Where(p => p.Toppic == toppic)
                .FirstOrDefaultAsync(cancellationToken); 
    }
}
