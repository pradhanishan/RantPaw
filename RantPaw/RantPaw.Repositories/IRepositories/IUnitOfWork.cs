using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IPostRepository Post { get; }

        Task SaveAsync();

    }
}
