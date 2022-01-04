using CoreApp102.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp102.Core.UnitOfWork
{
   public interface IUnitOfWork
    {
        IProductRepository product { get; }

        ICategoryRepository category { get; }

        Task CommitAsync();
        void Commit();
    }
}
