using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace TokenBucketRateLimiter;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.WebHost.CaptureStartupErrors(true);

        // Add services to the container.
        builder.ConfigureServices();

        WebApplication app = builder.Build();
        app.ConfigureMiddlewares();

        app.Run();
    }
}
