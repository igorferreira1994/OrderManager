using Microsoft.FeatureManagement;
using OrderManager.Application.Interfaces;
using OrderManager.Domain.Interfaces;
using OrderManager.Domain.Strategies;

namespace OrderManager.Application.Common;

public class TaxProvider(IFeatureManager featureManager) : ITaxProvider
{
    public async Task<ITaxStrategy> GetActiveStrategyAsync() =>
        await featureManager.IsEnabledAsync("ReformaTributaria")
            ? new ImpostoReformaStrategy()
            : new ImpostoVigorStrategy();
}