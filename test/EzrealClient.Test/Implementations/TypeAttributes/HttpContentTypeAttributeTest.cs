﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using EzrealClient.Implementations;
using EzrealClient.Implementations.TypeAttributes;
using Xunit;

namespace EzrealClient.Test.Implementations.TypeAttributes
{
    public class HttpContentTypeAttributeTest
    {
        public interface IMyApi
        {
            Task<HttpResponseMessage> PostAsync(HttpContent content);
        }

        [Fact]
        public async Task OnRequestAsyncTest()
        {
            var apiAction = new DefaultApiActionDescriptor(typeof(IMyApi).GetMethod("PostAsync"));
            var context = new TestRequestContext(apiAction, new StringContent("laojiu"));

            context.HttpContext.RequestMessage.RequestUri = new Uri("http://www.mywebapi.com");
            context.HttpContext.RequestMessage.Method = HttpMethod.Post;

            var attr = new HttpContentTypeAttribute();
            await attr.OnRequestAsync(new ApiParameterContext(context, 0));

            var body = await context.HttpContext.RequestMessage.Content.ReadAsStringAsync();
            Assert.True(body == "laojiu");
        }
    }
}
