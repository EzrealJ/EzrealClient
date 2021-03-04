using Xunit;

namespace EzrealClient.Test.BuildinExtensions
{
    public class HttpRequestHeaderExtensionsTest
    {
        [Fact]
        public void ToHeaderNameTest()
        {
            Assert.Equal("Accept", HttpRequestHeader.Accept.ToHeaderName());
            Assert.Equal("Accept-Charset", HttpRequestHeader.AcceptCharset.ToHeaderName());
        }
    }
}