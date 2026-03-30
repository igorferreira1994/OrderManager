using OrderManager.Domain.ValueObjects;

namespace OrderManager.Domain.Interfaces;

public interface ITaxStrategy
{
    Dinheiro Calcular(Dinheiro valorTotal);
}