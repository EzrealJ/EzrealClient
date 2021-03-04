using System.Threading.Tasks;
using EzrealClient.Attributes;

namespace EzrealClient.Benchmarks.Requests
{
    public interface IEzrealClientApi
    {
        [HttpGet("/benchmarks/{id}")]
        Task<Model> GetAsyc(string id);

        [HttpPost("/benchmarks")]
        Task<Model> PostJsonAsync([JsonContent] Model model);

        [HttpPut("/benchmarks/{id}")]
        Task<Model> PutFormAsync(string id, [FormContent] Model model);
    }
}
