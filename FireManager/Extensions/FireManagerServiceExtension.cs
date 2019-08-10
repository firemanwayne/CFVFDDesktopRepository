using FireManager.Interface;
using FireManager.Queries;
using FireManager.Services;
using FireManager.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FireManager.Extensions
{
    public static class FireManagerServiceExtension
    {
        public static void AddFireManager(this IServiceCollection services, Action<FireManagerOptions> options, bool RunTests)
        {
            services.Configure(options);

            services.AddHttpClient();

            services.AddScoped<IRequests, Requests>();
            services.AddScoped<IMemberRequest, MemberRequest>();
            services.AddScoped<IScheduleRequest, ScheduleRequest>();
            services.AddScoped<IPositionRequest, PositionRequest>();
            services.AddScoped<IStaffedPositionRequest, StaffedPositionRequest>();
            services.AddScoped<ITestRequests, TestRequests>();

            if (RunTests)          
                services.RunTests().Wait();            
        }
        
        private async static Task RunTests(this IServiceCollection servies)
        {
            var Provider = servies.BuildServiceProvider();

            using (var Scope = Provider.CreateScope())
            {
                var TestRequest = Scope.ServiceProvider.GetRequiredService<ITestRequests>();

                var Result = await TestRequest.RunTestSuite();
                string TestResult = Result ? "Succeeded" : "Failed";
                Console.WriteLine($"Test {TestResult}");
            }
        }
    }
}
