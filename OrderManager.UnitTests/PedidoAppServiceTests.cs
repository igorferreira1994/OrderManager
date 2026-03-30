using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Services;
using OrderManager.Domain.Interfaces;
using OrderManager.Domain.ValueObjects;
using Xunit;

namespace OrderManager.UnitTests.Application;

public class PedidoAppServiceTests
{
    private readonly IPedidoRepository _repository = Substitute.For<IPedidoRepository>();
    private readonly ITaxProvider _taxProvider = Substitute.For<ITaxProvider>();
    private readonly ITaxStrategy _taxStrategy = Substitute.For<ITaxStrategy>();
    private readonly PedidoAppService _service;

    public PedidoAppServiceTests()
    {
        _service = new PedidoAppService(_repository, _taxProvider, Substitute.For<ILogger<PedidoAppService>>());
    }

    [Fact]
    public async Task ProcessarAsync_DeveLancarExcecao_QuandoPedidoJaExiste()
    {
        // Arrange
        var request = new PedidoRequest(123, 1, new List<ItemRequest>());
        _repository.ExisteAsync(123).Returns(true);

        // Act
        var act = () => _service.ProcessarAsync(request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ProcessarAsync_DeveSalvarPedido_ComImpostoCalculado()
    {
        // Arrange
        var request = new PedidoRequest(1, 1, new List<ItemRequest> { new(101, 1, 100m) });
        _repository.ExisteAsync(1).Returns(false);
        _taxProvider.GetActiveStrategyAsync().Returns(_taxStrategy);
        _taxStrategy.Calcular(Arg.Any<Dinheiro>()).Returns(new Dinheiro(30m));

        // Act
        var response = await _service.ProcessarAsync(request);

        // Assert
        response.Status.Should().Be("Criado");
        await _repository.Received(1).SalvarAsync(Arg.Is<OrderManager.Domain.Entities.Pedido>(p => p.Imposto.Valor == 30m));
    }
}