using Microsoft.AspNetCore.Mvc;
using Supermercado.Application.DTOs.Cliente;
using Supermercado.Application.Interfaces;
using Supermercado.Domain.Entities;

namespace Supermercado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _repo;

    public ClienteController(IClienteRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Retorna todos os clientes cadastrados.
    /// </summary>
    /// <returns>Lista de clientes</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    /// <summary>
    /// Busca um cliente pelo ID.
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <returns>Cliente encontrado</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var cliente = await _repo.GetByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <param name="dto">Dados do cliente</param>
    /// <returns>Cliente criado</returns>
    [HttpPost]
    public async Task<ActionResult> Create(ClienteCreateDto dto)
    {
        var cliente = new Cliente(dto.Nome, dto.Email, dto.Telefone);

        await _repo.AddAsync(cliente);

        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    /// <summary>
    /// Atualiza um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="dto">Dados atualizados</param>
    /// <returns>Status da operação</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ClienteCreateDto dto)
    {
        var cliente = await _repo.GetByIdAsync(id);
        if (cliente == null) return NotFound();

        cliente.UpdateNome(dto.Nome);
        cliente.UpdateEmail(dto.Email);
        cliente.UpdateTelefone(dto.Telefone);

        await _repo.UpdateAsync(cliente);

        return NoContent();
    }

    /// <summary>
    /// Remove um cliente pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>Status da remoção</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var cliente = await _repo.GetByIdAsync(id);
        if (cliente == null) return NotFound();

        await _repo.DeleteAsync(id);
        return NoContent();
    }
}