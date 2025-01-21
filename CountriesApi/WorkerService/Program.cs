using Hangfire;
using JobLibrary.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        
        services.AddHangfire(configuration =>
        {
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                         .UseSimpleAssemblyNameTypeSerializer()
                         .UseRecommendedSerializerSettings()
                         .UseSqlServerStorage("Server=localhost;Database=HangfireDB;Trusted_Connection=True;TrustServerCertificate=True;");
        });

        
        services.AddHangfireServer();

        
        services.AddScoped<IUpdateJob, UpdateJob>();

        
        services.AddSingleton<IRecurringJobManager, RecurringJobManager>(); 
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var job = scope.ServiceProvider.GetRequiredService<IUpdateJob>();
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    
    recurringJobManager.AddOrUpdate<IUpdateJob>("update-countries-job", job => job.ExecuteAsync(), Cron.Hourly);
}

await host.RunAsync();
