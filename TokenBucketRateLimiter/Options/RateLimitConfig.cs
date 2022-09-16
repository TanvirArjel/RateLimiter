namespace TokenBucketRateLimiter.Options;

public sealed class RateLimitConfig
{
    public static readonly string OptionKey = "RateLimitConfig";

    public double RefillPeriod { get; set; }

    public int TokensPerPeriod { get; set; }

    public bool AutoRefill { get; set; }

    public int TokenLimit { get; set; }

    public int QueueLimit { get; set; }
}
