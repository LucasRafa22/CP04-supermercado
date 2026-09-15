namespace Supermercado.Application.DTOs.Venda;

public class VendaResponseDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal ValorTotal { get; set; }
}