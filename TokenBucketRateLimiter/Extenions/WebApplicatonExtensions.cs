using System;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using TokenBucketRateLimiter.Options;

namespace TokenBucketRateLimiter.Extenions;

public static class WebApplicatonExtensions
{
    private static string GetTicks()
    {
        return (DateTime.Now.Ticks & 0x11111).ToString("00000");
    }

    public static void UseTokenBucketRateLimiter(this WebApplication app)
    {
        if (app is null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        string tokenPolicy = "WeatherForcastLimitPolicy";
        RateLimitConfig rateLimitConfig = new RateLimitConfig();
        app.Configuration.GetSection(RateLimitConfig.OptionKey).Bind(rateLimitConfig);

        app.UseRateLimiter(new RateLimiterOptions()
            .AddTokenBucketLimiter(policyName: tokenPolicy, options =>
            {
                options.TokenLimit = rateLimitConfig.TokenLimit;
                options.QueueLimit = rateLimitConfig.QueueLimit;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(rateLimitConfig.RefillPeriod);
                options.TokensPerPeriod = rateLimitConfig.TokensPerPeriod;
                options.AutoReplenishment = rateLimitConfig.AutoRefill;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            }));
    }
}
