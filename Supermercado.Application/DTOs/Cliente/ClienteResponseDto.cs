namespace Supermercado.Application.DTOs.Cliente;

public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
}