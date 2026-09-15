using Microsoft.AspNetCore.Mvc;
using Supermercado.Application.DTOs.Categoria;
using Supermercado.Application.Interfaces;
using Supermercado.Domain.Entities;

namespace Supermercado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _repo;

    public CategoriaController(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Lista todas as categorias.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    /// <summary>
    /// Busca categoria por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var categoria = await _repo.GetByIdAsync(id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
    }

    /// <summary>
    /// Cria categoria.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(CategoriaCreateDto dto)
    {
        var categoria = new Categoria(dto.Nome, dto.Descricao);

        await _repo.AddAsync(categoria);

        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }

    /// <summary>
    /// Atualiza categoria.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, CategoriaCreateDto dto)
    {
        var categoria = await _repo.GetByIdAsync(id);
        if (categoria == null) return NotFound();

        categoria.UpdateNome(dto.Nome);
        categoria.UpdateDescricao(dto.Descricao);

        await _repo.UpdateAsync(categoria);

        return NoContent();
    }

    /// <summary>
    /// Remove categoria.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var categoria = await _repo.GetByIdAsync(id);
        if (categoria == null) return NotFound();

        await _repo.DeleteAsync(id);
        return NoContent();
    }
}