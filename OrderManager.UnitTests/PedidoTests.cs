using FluentAssertions;
using OrderManager.Domain.Entities;
using OrderManager.Domain.ValueObjects;
using Xunit;

namespace OrderManager.UnitTests;

public class PedidoTests
{
    [Fact]
    public void CalcularTotalItens_DeveSomarValoresCorretamente()
    {
        // Arrange
        var pedido = new Pedido(1, 100);
        var itens = new[]
        {
            new PedidoItem(101, 2, new Dinheiro(50m)),
            new PedidoItem(102, 1, new Dinheiro(25m))
        };

        // Act
        pedido.AdicionarItens(itens);
        var total = pedido.CalcularTotalItens();

        // Assert
        total.Valor.Should().Be(125m); // (2*50) + 25
    }
}