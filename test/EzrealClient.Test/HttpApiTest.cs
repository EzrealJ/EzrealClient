using Xunit;

namespace EzrealClient.Test
{
    public class HttpApiTest
    {


        public interface IGet
        {
            Task<string> Get();
        }

        public interface IPost
        {
            Task<string> Post();
        }

        public interface IMyApi : IGet, IPost
        {
            Task<int> Delete();
        }

        [Fact]
        public void GetAllApiMethodsTest()
        {
            var m1 = HttpApi.FindApiMethods(typeof(IMyApi));
            var m2 = HttpApi.FindApiMethods(typeof(IMyApi));

            Assert.False(object.ReferenceEquals(m1, m2));
            Assert.True(m1.Length == 3);
        }
    }
}
