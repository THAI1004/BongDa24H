using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class PricingRepository : IPricingRepository

{
    private readonly BongDa24HContext _context;
    public PricingRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<PricingRule> CreatePriceAsync(PricingRule pricingRule)
    {
        _context.PricingRules.Add(pricingRule);
        await _context.SaveChangesAsync();
        return pricingRule;
    }


    public async Task<List<PricingRule>> GetPriceAsync(int productId)
    {
        return await _context.PricingRules.Where(p => p.PitchId == productId).ToListAsync();


    }

    public async Task<bool> UpdatePriceAsync(PricingRule pricingRule)
    {
        _context.PricingRules.Update(pricingRule);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> DeletePriceAsync(int pricingRuleId)
    {
        var pricingRule = await _context.PricingRules.FindAsync(pricingRuleId);
        if (pricingRule == null)
        {
            return false;
        }
        _context.PricingRules.Remove(pricingRule);
        return await _context.SaveChangesAsync() > 0;
    }
}