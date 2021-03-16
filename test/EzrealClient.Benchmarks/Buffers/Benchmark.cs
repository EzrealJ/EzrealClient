﻿using BenchmarkDotNet.Attributes;
using EzrealClient.Internals;

namespace EzrealClient.Benchmarks.Buffers
{
    public class Benchmark : IBenchmark
    {
        [Benchmark]
        public void Rent()
        {
            using (new RecyclableBufferWriter<byte>()) { }
        }

        [Benchmark]
        public void New()
        {
            _ = new byte[1024];
        }
    }
}