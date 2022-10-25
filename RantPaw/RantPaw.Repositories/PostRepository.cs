using Microsoft.EntityFrameworkCore;
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
    public sealed class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PostWithPostReaction>> GetPostsWithReactions()
        {
            List<PostReaction> postReactions = await _db.PostReactions.Include(pr => pr.User).Include(pr => pr.Reaction).ToListAsync();

            List<Post> posts = await _db.Posts.ToListAsync();

            List<PostWithPostReaction> postsWithPostReaction = new();

            foreach (Post post in posts)
            {
                PostWithPostReaction postWithPostReaction = new PostWithPostReaction()
                {
                    AuthorId = post.AuthorID,
                    Description = post.Description,
                    IsAnonymous = post.IsAnonymous,
                    Reactions = postReactions.Where(pr => pr.PostId == post.Id).ToList(),
                    CreatedDate = post.CreatedDate,
                    UpdateDate = post.UpdateDate
                };

                postsWithPostReaction.Add(postWithPostReaction);
            }

            return postsWithPostReaction.AsEnumerable();




        }
    }
}
