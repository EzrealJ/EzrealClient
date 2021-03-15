using Refit;
using System.Threading.Tasks;

namespace EzrealClient.Benchmarks.Requests
{
    public interface IRefitApi
    {
        [Get("/benchmarks/{id}")]
        System.Threading.Tasks.Task<Model> GetAsyc(string id);

        [Post("/benchmarks")]
        System.Threading.Tasks.Task<Model> PostJsonAsync([Body(BodySerializationMethod.Serialized)]Model model);

        [Put("/benchmarks/{id}")]
        System.Threading.Tasks.Task<Model> PutFormAsync(string id, [Body(BodySerializationMethod.UrlEncoded)]Model model);
    }
}
