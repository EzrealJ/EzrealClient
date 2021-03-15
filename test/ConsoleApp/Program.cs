
using EzrealClient.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(builder => builder.AddConsole());
            services
                    .AddEzrealClient()
                    .UseJsonFirstApiActionDescriptor()
                    .UseFluentConfigure(builder =>
                    {
                        builder.TryAddApiFilterAttribute(new LoggingFilterAttribute());
                        builder.TryAddApiActionAttribute(new HttpHostAttribute("http://www.baidu.com"));
                        builder.SetCacheAttribute(new CacheAttribute(500));
                        builder.ConfigureInterface<ITestInterface>(interfaceBuilder =>
                        {
                            interfaceBuilder.SetCacheAttribute(new CacheAttribute(1000));
                            interfaceBuilder.ConfigureMethod(nameof(ITestInterface.Post), methodBuilder =>
                            {
                                //methodBuilder.
                                methodBuilder.SetCacheAttribute(new CacheAttribute(5000));
                                methodBuilder
                                .ConfigureParameter("name", parameterBuilder =>
                                {
                                    parameterBuilder.AliasAs("NewName");
                                }).ConfigureParameter("testClass", parameterBuilder =>
                                {
                                    parameterBuilder.AddApiParameterAttribute(new JsonContentAttribute());
                                });
                                methodBuilder.TryAddApiActionAttribute(new HttpPostAttribute("api/PostTest"));
                            });


                        });


                    });
            services.AddHttpApi<ITestInterface>();
            var sp = services.BuildServiceProvider();
            var i = sp.GetRequiredService<ITestInterface>();
            i.Post("testValue", new TestClass { Age = 10, Name = "xiaoming" });
            Console.ReadKey();
        }




    }

    public class TestClass
    {

        public int Age { get; set; }

        public string Name { get; set; }
    }

    public interface ITestInterface
    {
        Task<string> Post(string name, TestClass testClass);
    }
}
