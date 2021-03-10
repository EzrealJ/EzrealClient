using EzrealClient.Implementations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi
{
    /// <summary>
    /// 支持FluentApi方式的ApiActionDescriptor提供者的接口
    /// </summary>
    public class FluentApiActionDescriptorProvider : DefaultApiActionDescriptorProvider
    {
        public override ApiActionDescriptor CreateActionDescriptor(MethodInfo method, Type interfaceType)
        {
            throw new NotImplementedException();
        }
    }
}
