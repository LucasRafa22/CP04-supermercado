using Supermercado.Domain.Entities;

namespace Supermercado.Application.Interfaces;

public interface IItemVendaRepository
{
    Task<List<ItemVenda>> GetAllAsync();
    Task<ItemVenda?> GetByIdAsync(Guid id);
    Task AddAsync(ItemVenda itemVenda);
    Task UpdateAsync(ItemVenda itemVenda);
    Task DeleteAsync(Guid id);
}