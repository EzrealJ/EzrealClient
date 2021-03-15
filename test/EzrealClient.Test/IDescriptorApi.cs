﻿using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EzrealClient.Attributes;

namespace EzrealClient.Test
{
    /// <summary>
    /// 用户操作接口
    /// </summary>
    [LoggingFilter]
    [HttpHost("http://localhost")]
    public interface IDescriptorApi : IHttpApi
    {
        [HttpGet]
        [Timeout(10 * 1000)]
        System.Threading.Tasks.Task<string> Get1([Uri] string url, string something);

        [HttpGet]
        System.Threading.Tasks.Task<HttpResponseMessage> Get2([Required]string id, CancellationToken token);

        [HttpGet]
        System.Threading.Tasks.Task<Stream> Get3([Required]string account, CancellationToken token);
         
        [HttpPost]
        Task Get4();

        [HttpGet]
        System.Threading.Tasks.Task<object> Get5(string nickName);


        [HttpGet]
        System.Threading.Tasks.Task<byte[]> Get6(string nickName);

    }
}
