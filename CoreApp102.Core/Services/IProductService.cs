using CoreApp102.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp102.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<Product> GetWithByIdAsync(int proId);

        //Product ile ilgili tanimlamam gerekene ic metodlarim var ise onlari bu servis katmaninda  yazabilirim.
    }
}
