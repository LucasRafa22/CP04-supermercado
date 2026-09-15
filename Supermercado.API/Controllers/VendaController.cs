using Microsoft.AspNetCore.Mvc;
using Supermercado.Application.DTOs.Venda;
using Supermercado.Application.Interfaces;
using Supermercado.Domain.Entities;

namespace Supermercado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaRepository _repo;

    public VendaController(IVendaRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Lista todas as vendas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    /// <summary>
    /// Busca venda por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var venda = await _repo.GetByIdAsync(id);
        if (venda == null) return NotFound();
        return Ok(venda);
    }

    /// <summary>
    /// Cria venda.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(VendaCreateDto dto)
    {
        var venda = new Venda(dto.ClienteId);

        await _repo.AddAsync(venda);

        return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
    }

    /// <summary>
    /// Remove venda.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}