namespace LocationExplorer.Web
{
    using Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).MigrateAndSeedDatabase().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(option
                    => option.ValidateScopes = false)
                .Build();
    }
}
