using RantPaw.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Repositories.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {

        Task<IEnumerable<PostWithPostReaction>> GetPostsWithReactions();
        Task<IEnumerable<PostWithPostReaction>> GetPostsWithReactionsBetween(int startingRow, int numberOfRows);
    }
}
