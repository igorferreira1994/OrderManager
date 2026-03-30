using OrderManager.Application.DTOs;

namespace OrderManager.Application.Interfaces;

public interface IPedidoAppService
{
    Task<PedidoResponse> ProcessarAsync(PedidoRequest request);
    Task<PedidoDetalhadoResponse?> ObterPorIdAsync(int id);
    Task<IEnumerable<PedidoDetalhadoResponse>> ListarPorStatusAsync(string status);
}