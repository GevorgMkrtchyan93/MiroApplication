namespace Miro.Server.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<int> GetCountAsync();
    }
}
