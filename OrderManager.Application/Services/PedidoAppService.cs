using Microsoft.Extensions.Logging;
using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Interfaces;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _repository;
        private readonly ITaxProvider _taxProvider;
        private readonly ILogger<PedidoAppService> _logger;

        public PedidoAppService(
            IPedidoRepository repository,
            ITaxProvider taxProvider,
            ILogger<PedidoAppService> logger)
        {
            _repository = repository;
            _taxProvider = taxProvider;
            _logger = logger;
        }

        public async Task<PedidoResponse> ProcessarAsync(PedidoRequest request)
        {
            if (await _repository.ExisteAsync(request.PedidoId))
            {
                _logger.LogWarning("Pedido duplicado detectado: {Id}", request.PedidoId);
                throw new InvalidOperationException($"Pedido {request.PedidoId} já existe.");
            }

            var pedido = new Pedido(request.PedidoId, request.ClienteId);

            var itensDominio = request.Itens.Select(i =>
                new PedidoItem(i.ProdutoId, i.Quantidade, new Dinheiro(i.Valor))).ToList();

            pedido.AdicionarItens(itensDominio);

            var strategy = await _taxProvider.GetActiveStrategyAsync();
            var valorTotal = pedido.CalcularTotalItens();
            pedido.AplicarImposto(strategy.Calcular(valorTotal));

            await _repository.SalvarAsync(pedido);

            _logger.LogInformation("Pedido {Id} processado com sucesso.", pedido.PedidoId);

            return new PedidoResponse(pedido.PedidoId, pedido.Status);
        }

        public async Task<PedidoDetalhadoResponse?> ObterPorIdAsync(int id)
        {
            var p = await _repository.ObterPorIdAsync(id);

            if (p == null) return null;

            return new PedidoDetalhadoResponse(
                p.PedidoId,
                p.PedidoId,
                p.ClienteId,
                p.Imposto.Valor,
                p.Status,
                p.Itens.Select(i => new ItemRequest(i.ProdutoId, i.Quantidade, i.ValorUnitario.Valor)).ToList()
            );
        }

        public async Task<IEnumerable<PedidoDetalhadoResponse>> ListarPorStatusAsync(string status)
        {
            var pedidos = await _repository.ListarPorStatusAsync(status);

            var resultado = pedidos.Select(p => new PedidoDetalhadoResponse(
                p.PedidoId,
                p.PedidoId,
                p.ClienteId,
                p.Imposto.Valor,
                p.Status,
                p.Itens.Select(i => new ItemRequest(i.ProdutoId, i.Quantidade, i.ValorUnitario.Valor)).ToList()
            )).ToList();

            return resultado;
        }
    }
}