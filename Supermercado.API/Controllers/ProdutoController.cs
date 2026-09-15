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
    private readonly ILogger<ProdutoController> _logger;

    public ProdutoController(IProdutoRepository repo, ILogger<ProdutoController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos os produtos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation("GET /produtos iniciado | TraceId: {TraceId}", traceId);

        var result = await _repo.GetAllAsync();

        _logger.LogInformation("GET /produtos finalizado | TraceId: {TraceId} | Total: {Count}",
            traceId, result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Busca produto por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation("GET /produtos/{Id} iniciado | TraceId: {TraceId}", id, traceId);

        var produto = await _repo.GetByIdAsync(id);

        if (produto == null)
        {
            _logger.LogWarning("Produto não encontrado | TraceId: {TraceId} | Id: {Id}",
                traceId, id);

            return NotFound();
        }

        _logger.LogInformation("Produto encontrado | TraceId: {TraceId} | Id: {Id}",
            traceId, id);

        return Ok(produto);
    }

    /// <summary>
    /// Cria produto.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(ProdutoCreateDto dto)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation("POST /produtos iniciado | TraceId: {TraceId}", traceId);

        var produto = new Produto(dto.Nome, dto.Preco, dto.Estoque, dto.CategoriaId);

        await _repo.AddAsync(produto);

        _logger.LogInformation("Produto criado | TraceId: {TraceId} | Id: {Id}",
            traceId, produto.Id);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza produto.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ProdutoCreateDto dto)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation("PUT /produtos/{Id} iniciado | TraceId: {TraceId}", id, traceId);

        var produto = await _repo.GetByIdAsync(id);

        if (produto == null)
        {
            _logger.LogWarning("Update falhou - produto não encontrado | TraceId: {TraceId} | Id: {Id}",
                traceId, id);

            return NotFound();
        }

        produto.UpdateNome(dto.Nome);
        produto.UpdatePreco(dto.Preco);
        produto.UpdateEstoque(dto.Estoque);

        await _repo.UpdateAsync(produto);

        _logger.LogInformation("Produto atualizado | TraceId: {TraceId} | Id: {Id}",
            traceId, id);

        return NoContent();
    }

    /// <summary>
    /// Remove produto.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation("DELETE /produtos/{Id} iniciado | TraceId: {TraceId}", id, traceId);

        var produto = await _repo.GetByIdAsync(id);

        if (produto == null)
        {
            _logger.LogWarning("Delete falhou - produto não encontrado | TraceId: {TraceId} | Id: {Id}",
                traceId, id);

            return NotFound();
        }

        await _repo.DeleteAsync(id);

        _logger.LogInformation("Produto removido | TraceId: {TraceId} | Id: {Id}",
            traceId, id);

        return NoContent();
    }
}