using Microsoft.AspNetCore.Mvc;
using Supermercado.Application.DTOs.Produto;
using Supermercado.Application.Interfaces;
using Supermercado.Domain.Entities;

namespace Supermercado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _repo;

    public ProdutoController(IProdutoRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Lista todos os produtos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    /// <summary>
    /// Busca produto por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var produto = await _repo.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    /// <summary>
    /// Cria produto.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(ProdutoCreateDto dto)
    {
        var produto = new Produto(dto.Nome, dto.Preco, dto.Estoque, dto.CategoriaId);

        await _repo.AddAsync(produto);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza produto.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ProdutoCreateDto dto)
    {
        var produto = await _repo.GetByIdAsync(id);
        if (produto == null) return NotFound();

        produto.UpdateNome(dto.Nome);
        produto.UpdatePreco(dto.Preco);
        produto.UpdateEstoque(dto.Estoque);

        await _repo.UpdateAsync(produto);

        return NoContent();
    }

    /// <summary>
    /// Remove produto.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var produto = await _repo.GetByIdAsync(id);
        if (produto == null) return NotFound();

        await _repo.DeleteAsync(id);
        return NoContent();
    }
}