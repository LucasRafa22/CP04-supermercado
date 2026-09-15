namespace Supermercado.Application.DTOs.Produto;

public class ProdutoResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public Guid CategoriaId { get; set; }
}