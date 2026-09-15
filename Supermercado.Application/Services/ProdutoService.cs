using Supermercado.Domain.Entities;
using Supermercado.Application.Interfaces;

namespace Supermercado.Application.Services;

public class ProdutoService
{
    private readonly IRepository<Produto> _repository;

    public ProdutoService(IRepository<Produto> repository)
    {
        _repository = repository;
    }

    public async Task AddProdutoAsync(Produto produto)
    {
        if (produto.Preco <= 0)
            throw new Exception("Preço inválido");

        await _repository.Add(produto);
    }

    public async Task DeleteProdutoAsync(Guid id)
    {
        var exists = await _repository.GetById(id);

        if (exists == null)
            throw new Exception("Produto não encontrado");

        await _repository.Delete(id);
    }
}