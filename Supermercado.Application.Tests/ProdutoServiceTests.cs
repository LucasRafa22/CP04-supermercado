using Moq;
using Supermercado.Application.Interfaces;
using Supermercado.Application.Services;
using Supermercado.Domain.Entities;
using Xunit;

namespace Supermercado.Application.Tests;

public class ProdutoServiceTests
{
    [Fact]
    public async Task AddProduto_Com_Preco_Valido_Deve_Chamar_Repository()
    {
        var repoMock = new Mock<IRepository<Produto>>();
        var service = new ProdutoService(repoMock.Object);

        var produto = new Produto(
            "Arroz",
            10,
            5,
            Guid.NewGuid()
        );

        await service.AddProdutoAsync(produto);

        repoMock.Verify(r => r.Add(produto), Times.Once);
    }

    [Fact]
    public async Task AddProduto_Com_Preco_Zerado_Nao_Deve_Chamar_Repository()
    {
        var repoMock = new Mock<IRepository<Produto>>();
        var service = new ProdutoService(repoMock.Object);

        var ex = await Assert.ThrowsAsync<Exception>(() =>
            service.AddProdutoAsync(new Produto(
                "Arroz",
                0,
                5,
                Guid.NewGuid()
            ))
        );

        Assert.Equal("Preço deve ser maior que zero.", ex.Message);

        repoMock.Verify(r => r.Add(It.IsAny<Produto>()), Times.Never);
    }

    [Fact]
    public async Task DeleteProduto_Inexistente_Deve_Lancar_Excecao()
    {
        var repoMock = new Mock<IRepository<Produto>>();
        var service = new ProdutoService(repoMock.Object);

        repoMock
            .Setup(r => r.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Produto?)null);

        var ex = await Assert.ThrowsAsync<Exception>(() =>
            service.DeleteProdutoAsync(Guid.NewGuid())
        );

        Assert.Equal("Produto não encontrado", ex.Message);

        repoMock.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Never);
    }
}