namespace Supermercado.Application.DTOs.ItemVenda;

public class ItemVendaResponseDto
{
    public Guid Id { get; set; }
    public Guid VendaId { get; set; }
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}