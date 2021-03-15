using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EzrealClient.Benchmarks.Requests
{
    /// <summary> 
    /// 跳过真实的http请求环节的模拟Post json请求
    /// </summary>
    public class PostJsonBenchmark : Benchmark
    {
        /// <summary>
        /// 使用原生HttpClient请求
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public async System.Threading.Tasks.Task<Model> HttpClient_PostJsonAsync()
        {
            using var scope = this.ServiceProvider.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(HttpClient).FullName);

            var input = new Model { A = "a" };
            var json = JsonSerializer.SerializeToUtf8Bytes(input);
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://webapiclient.com/")
            {
                Content = new ByteArrayJsonContent(json)
            };

            var response = await httpClient.SendAsync(request);
            json = await response.Content.ReadAsUtf8ByteArrayAsync();
            return JsonSerializer.Deserialize<Model>(json);
        }

        /// <summary>
        /// 使用EzrealClient请求
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public async System.Threading.Tasks.Task<Model> EzrealClient_PostJsonAsync()
        {
            using var scope = this.ServiceProvider.CreateScope();
            var banchmarkApi = scope.ServiceProvider.GetRequiredService<IEzrealClientApi>();
            var input = new Model { A = "a" };
            return await banchmarkApi.PostJsonAsync(input);
        }


        [Benchmark]
        public async System.Threading.Tasks.Task<Model> Refit_PostJsonAsync()
        {
            using var scope = this.ServiceProvider.CreateScope();
            var banchmarkApi = scope.ServiceProvider.GetRequiredService<IRefitApi>();
            var input = new Model { A = "a" };
            return await banchmarkApi.PostJsonAsync(input);
        }
    }
}
