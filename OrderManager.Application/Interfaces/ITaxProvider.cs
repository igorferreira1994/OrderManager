using OrderManager.Domain.Interfaces;

namespace OrderManager.Application.Interfaces;

public interface ITaxProvider
{
    Task<ITaxStrategy> GetActiveStrategyAsync();
}