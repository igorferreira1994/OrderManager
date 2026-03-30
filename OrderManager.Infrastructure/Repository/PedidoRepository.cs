using System.Collections.Concurrent;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Interfaces;

namespace OrderManager.Infrastructure.Persistence;

public class PedidoRepository : IPedidoRepository
{
    private static readonly ConcurrentDictionary<int, Pedido> _db = new();

    public Task<bool> ExisteAsync(int id) => Task.FromResult(_db.ContainsKey(id));
    public Task SalvarAsync(Pedido p) { _db.TryAdd(p.PedidoId, p); return Task.CompletedTask; }
    public Task<Pedido?> ObterPorIdAsync(int id) { _db.TryGetValue(id, out var p); return Task.FromResult(p); }
    public Task<IEnumerable<Pedido>> ListarPorStatusAsync(string s) =>
        Task.FromResult(_db.Values.Where(x => x.Status.Equals(s, StringComparison.OrdinalIgnoreCase)));
}