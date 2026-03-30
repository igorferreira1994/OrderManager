using OrderManager.Domain.ValueObjects;

namespace OrderManager.Domain.Entities;

public class Pedido
{
    public int PedidoId { get; private set; }
    public int ClienteId { get; private set; }
    public Dinheiro Imposto { get; private set; } = Dinheiro.Zero;
    public string Status { get; private set; } = "Criado";
    private readonly List<PedidoItem> _itens = new();
    public IReadOnlyCollection<PedidoItem> Itens => _itens;

    public Pedido(int pedidoId, int clienteId)
    {
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }

    public void AdicionarItens(IEnumerable<PedidoItem> itens) => _itens.AddRange(itens);

    public void AplicarImposto(Dinheiro imposto) => Imposto = imposto;

    public Dinheiro CalcularTotalItens() =>
        new(_itens.Sum(i => i.ValorUnitario.Valor * i.Quantidade));
}

public record PedidoItem(int ProdutoId, int Quantidade, Dinheiro ValorUnitario);