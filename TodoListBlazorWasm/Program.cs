using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBlazoredToast();
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddTransient<ITaskApiClient, TaskApiClient>();
            builder.Services.AddTransient<IUserApiClient, UserApiClient>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendApiuri"])});

            await builder.Build().RunAsync();
        }
    }
}
