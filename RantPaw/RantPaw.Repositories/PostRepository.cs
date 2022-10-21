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
    }
}
