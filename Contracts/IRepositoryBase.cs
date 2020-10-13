using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaveManager.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<ICollection<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<bool> IsExistsAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}