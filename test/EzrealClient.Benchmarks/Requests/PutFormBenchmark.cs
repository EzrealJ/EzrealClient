using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EzrealClient.Benchmarks.Requests
{
    /// <summary> 
    /// 跳过真实的http请求环节的模拟Post表单请求
    /// </summary>
    public class PutFormBenchmark : Benchmark
    {
        /// <summary>
        /// 使用EzrealClient请求
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public async Task<Model> EzrealClient_PutFormAsync()
        {
            using var scope = this.ServiceProvider.CreateScope();
            var banchmarkApi = scope.ServiceProvider.GetRequiredService<IEzrealClientApi>();
            var input = new Model { A = "a" };
            return await banchmarkApi.PutFormAsync("id001", input);
        }


        [Benchmark]
        public async Task<Model> Refit_PutFormAsync()
        {
            using var scope = this.ServiceProvider.CreateScope();
            var banchmarkApi = scope.ServiceProvider.GetRequiredService<IRefitApi>();
            var input = new Model { A = "a" };
            return await banchmarkApi.PutFormAsync("id001", input);
        }
    }
}
