using RantPaw.DataContext;
using RantPaw.Models.Entities;
using RantPaw.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Repositories
{
    public sealed class ReactionRepository : Repository<PostReaction>, IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
