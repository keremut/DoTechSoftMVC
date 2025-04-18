using DoTechSoftMVC.Data.Concrete.Entities;

namespace DoTechSoftMVC.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T service);
        Task UpdateAsync(T service);
        Task DeleteAsync(int id);
    }
}
