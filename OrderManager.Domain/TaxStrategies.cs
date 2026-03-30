using OrderManager.Domain.Interfaces;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Domain.Strategies;

public class ImpostoVigorStrategy : ITaxStrategy
{
    public Dinheiro Calcular(Dinheiro valorTotal) => new(valorTotal.Valor * 0.3m);
}

public class ImpostoReformaStrategy : ITaxStrategy
{
    public Dinheiro Calcular(Dinheiro valorTotal) => new(valorTotal.Valor * 0.2m);
}