namespace Supermercado.Application.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task Add(T entity);
    Task Delete(Guid id);
}