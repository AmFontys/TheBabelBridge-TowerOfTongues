using BBTT.BenchmarkClassCrossword;
using BBTT.CrosswordAPI.Controllers;
using BBTT.CrosswordCore;
using BBTT.Files;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BBTT.BenchMarkingConsole
{
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte [] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly SHA384 md5 = SHA384.Create();

        public Md5VsSha256 ()
        {
            data = new byte [ N ];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte [] Sha256 () => sha256.ComputeHash(data);

        [Benchmark]
        public byte [] Md5 () => md5.ComputeHash(data);
    }
    public static class Program
    {
        private static readonly ServiceProvider _serviceProvider = ConfigureServices();

        public static void Main (string [] args)
        {
            //_serviceProvider.GetServices<CrosswordCoreBenchmark>();
            //var summary = BenchmarkRunner.Run<Md5VsSha256>();

            CrosswordCoreBenchmark crosswordCoreBenchmark = _serviceProvider.GetRequiredService<CrosswordCoreBenchmark>();
            CrosswordLangComparison crosswordLangComparison = _serviceProvider.GetRequiredService<CrosswordLangComparison>();
            if (crosswordCoreBenchmark == null || crosswordLangComparison == null)
            {
                throw new InvalidOperationException("CrosswordCoreBenchmark or CrosswordLangComparison service is not registered.");
            }
            var summary = BenchmarkRunner.Run<CrosswordCoreBenchmark>();
            var summary2 = BenchmarkRunner.Run<CrosswordLangComparison>();

            

        }
        

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register dependencies for CrosswordCoreBenchmark    
            services.AddSingleton<ICrosswordAccesor, CrosswordAccesor>();
            services.AddSingleton<ICrosswordGenerator, CrosswordGenerator>();
            services.AddSingleton<ICsvReaderAcessor, CsvReaderAcessor>();
            services.AddSingleton<ILogger<CrosswordController>>(provider =>
                LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<CrosswordController>());
            services.AddSingleton<CrosswordCoreBenchmark>();

            // Register dependencies for CrosswordLangComparison
            services.AddSingleton<CrosswordLangComparison>();

            return services.BuildServiceProvider();
        }
    }
}
