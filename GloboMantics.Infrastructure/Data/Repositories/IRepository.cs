namespace GloboMantics.Infrastructure.Data.Repositories;

public interface IRepository<T>
{
    Task<T> CreateAsync(Guid id);
    Task<T> FindByAsync(string value);
    Task<IEnumerable<T>> AllAsync();
    Task AddAsync(T item);
    Task SaveChangesAsync();
    Task<T> GetAsync(Guid id);
}