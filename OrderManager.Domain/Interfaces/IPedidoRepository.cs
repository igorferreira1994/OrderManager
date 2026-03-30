using OrderManager.Domain.Entities;

namespace OrderManager.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<bool> ExisteAsync(int id);
    Task SalvarAsync(Pedido pedido);
    Task<Pedido?> ObterPorIdAsync(int id);
    Task<IEnumerable<Pedido>> ListarPorStatusAsync(string status);
}