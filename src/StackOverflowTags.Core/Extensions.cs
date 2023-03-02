using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace StackOverflowTags.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediator(q => q.ServiceLifetime = ServiceLifetime.Transient);
        services.AddHttpClient(
            "stackExchangeApi",
            q => q.BaseAddress = new("https://api.stackexchange.com/2.3/")
        )
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression =
                    DecompressionMethods.GZip | 
                    DecompressionMethods.Deflate
            })
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(q => q.StatusCode is System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(
                    6,
                    q => TimeSpan.FromSeconds(Math.Pow(
                        2,
                        q
                    ))
                )
            );

        return services;
    }

    public static async Task<T> Get<T>(
        this HttpClient http,
        string url,
        JsonTypeInfo<T> jsonTypeInfo,
        CancellationToken ct = default
    )
    {
        var httpRes = await http.GetAsync(
            url,
            ct
        );

        httpRes.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize(
            await httpRes.Content.ReadAsStringAsync(),
            jsonTypeInfo
        );
    }
}