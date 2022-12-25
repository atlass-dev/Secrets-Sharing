using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<bool> Delete(T entity);
    }
}
