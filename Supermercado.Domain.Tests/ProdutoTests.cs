using Xunit;
using Supermercado.Domain.Entities;
using System;

namespace Supermercado.Domain.Tests;

public class ProdutoTests
{
    // ✔ TESTE 1 - FACT (caminho feliz)
    [Fact]
    public void Criar_Produto_Valido_Deve_Ser_Criado_Corretamente()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();

        // Act
        var produto = new Produto(
            nome: "Arroz",
            preco: 10.50m,
            estoque: 5,
            categoriaId: categoriaId
        );

        // Assert
        Assert.Equal("Arroz", produto.Nome);
        Assert.Equal(10.50m, produto.Preco);
        Assert.Equal(5, produto.Estoque);
        Assert.Equal(categoriaId, produto.CategoriaId);
    }

    // ✔ TESTE 2 - THEORY (validação de erro)
    [Theory]
    [InlineData("", 10, 5)]
    [InlineData("   ", 10, 5)]
    public void Criar_Produto_Com_Nome_Invalido_Deve_Lancar_Excecao(string nome, decimal preco, int estoque)
    {
        // Arrange
        var categoriaId = Guid.NewGuid();

        // Act & Assert
        var ex = Assert.Throws<Exception>(() =>
        {
            new Produto(nome, preco, estoque, categoriaId);
        });

        Assert.Equal("Nome do produto inválido.", ex.Message);
    }

    // ✔ TESTE 3 - PREÇO INVÁLIDO
    [Fact]
    public void Criar_Produto_Com_Preco_Zerado_Deve_Lancar_Excecao()
    {
        var categoriaId = Guid.NewGuid();

        var ex = Assert.Throws<Exception>(() =>
        {
            new Produto("Arroz", 0, 5, categoriaId);
        });

        Assert.Equal("Preço deve ser maior que zero.", ex.Message);
    }

    // ✔ TESTE 4 - ESTOQUE INVÁLIDO
    [Fact]
    public void Criar_Produto_Com_Estoque_Negativo_Deve_Lancar_Excecao()
    {
        var categoriaId = Guid.NewGuid();

        var ex = Assert.Throws<Exception>(() =>
        {
            new Produto("Arroz", 10, -1, categoriaId);
        });

        Assert.Equal("Estoque inválido.", ex.Message);
    }
}