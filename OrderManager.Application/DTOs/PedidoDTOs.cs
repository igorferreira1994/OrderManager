using System.Collections.Generic;

namespace OrderManager.Application.DTOs
{
    public record PedidoRequest(int PedidoId, int ClienteId, List<ItemRequest> Itens);

    public record ItemRequest(int ProdutoId, int Quantidade, decimal Valor);

    public record PedidoResponse(int Id, string Status);
  
    public record PedidoDetalhadoResponse(
        int Id,
        int PedidoId,
        int ClienteId,
        decimal Imposto,
        string Status,
        List<ItemRequest> Itens);
}