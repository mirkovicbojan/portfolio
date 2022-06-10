using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Contexts;


namespace IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TimeSheetContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<TimeSheetContext>(options =>
                {
                    options
                        .UseLazyLoadingProxies()
                        .UseInMemoryDatabase("InMemoryCategoryDb");

                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<TimeSheetContext>())
                {
                    try
                    {
                        Utilities.InitializeDbForTests(appContext);
                        appContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        //throw;
                    }
                }
            });
        }
    }
}