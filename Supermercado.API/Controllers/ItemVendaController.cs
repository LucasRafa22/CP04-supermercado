using Microsoft.AspNetCore.Mvc;
using Supermercado.Application.DTOs.ItemVenda;
using Supermercado.Application.Interfaces;
using Supermercado.Domain.Entities;

namespace Supermercado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemVendaController : ControllerBase
{
    private readonly IItemVendaRepository _repo;

    public ItemVendaController(IItemVendaRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Lista itens de venda.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    /// <summary>
    /// Busca item por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Cria item de venda.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(ItemVendaCreateDto dto)
    {
        var item = new ItemVenda(dto.VendaId, dto.ProdutoId, dto.Quantidade, dto.PrecoUnitario);

        await _repo.AddAsync(item);

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    /// <summary>
    /// Remove item de venda.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}