namespace Supermercado.Application.DTOs.Produto;

public class ProdutoCreateDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public Guid CategoriaId { get; set; }
}