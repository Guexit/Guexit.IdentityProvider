using Azure.Monitor.OpenTelemetry.Exporter;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Guexit.IdentityProvider.WebApi.DependencyInjection;

public static class OpenTelemetryInstaller
{
    public static void AddTelemetry(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("ApplicationInsights");
        if (string.IsNullOrWhiteSpace(connectionString))
            return;

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName));
            options.AddAzureMonitorLogExporter(o => o.ConnectionString = connectionString);
        });
        
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(b => b.AddService(builder.Environment.ApplicationName))
            .WithTracing(b => b
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName)
                .AddAzureMonitorTraceExporter(o => o.ConnectionString = connectionString))
            .WithMetrics(b => b
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter(MassTransit.Monitoring.InstrumentationOptions.MeterName)
                .AddAzureMonitorMetricExporter(o => o.ConnectionString = connectionString));
    }
}
