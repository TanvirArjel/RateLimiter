using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokenBucketRateLimiter.Extenions;

namespace TokenBucketRateLimiter;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void ConfigureMiddlewares(this WebApplication app)
    {
        if (app is null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Add token bucket rate limiter.
        app.UseTokenBucketRateLimiter();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
    }
}
