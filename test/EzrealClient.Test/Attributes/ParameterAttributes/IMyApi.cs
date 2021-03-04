using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EzrealClient.Test.Attributes.ParameterAttributes
{
    public interface IMyApi
    {
        ITask<HttpResponseMessage> PostAsync(object headers);
    }
}
