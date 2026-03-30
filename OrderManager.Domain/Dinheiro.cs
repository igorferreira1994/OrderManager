namespace OrderManager.Domain.ValueObjects;

public record Dinheiro(decimal Valor)
{
    public static Dinheiro Zero => new(0);
    public static Dinheiro DeDecimal(decimal valor) => new(Math.Round(valor, 2));
}