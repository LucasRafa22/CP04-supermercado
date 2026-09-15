using Supermercado.Domain.Entities;

namespace Supermercado.Application.Interfaces;

public interface IVendaRepository
{
    Task<List<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(Guid id);
    Task AddAsync(Venda venda);
    Task UpdateAsync(Venda venda);
    Task DeleteAsync(Guid id);
}