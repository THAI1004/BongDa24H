using Backend.Models;

namespace Backend.Interfaces;

public interface IPricingRepository
{
    Task<List<PricingRule>> GetPriceAsync(int productId);
    Task<PricingRule> CreatePriceAsync(PricingRule pricingRule);
    Task<bool> UpdatePriceAsync(PricingRule pricingRule);
    Task<bool> DeletePriceAsync(int pricingRuleId);
}