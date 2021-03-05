﻿using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;
using EzrealClient;

namespace EzrealClient.Benchmarks.StringReplaces
{
    [MemoryDiagnoser]
    public class Benchmark : IBenchmark
    {
        private readonly string str = "EzrealClient.Benchmarks.StringReplaces.EzrealClient";
        private readonly string pattern = "core";
        private readonly string replacement = "CORE";

        [Benchmark]
        public void ReplaceByRegexNew()
        {
            new Regex(pattern, RegexOptions.IgnoreCase).Replace(str, replacement);           
        }

        [Benchmark]
        public void ReplaceByRegexStatic()
        {
            Regex.Replace(str, pattern, replacement, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public void ReplaceByCutomSpan()
        {
            str.RepaceIgnoreCase(pattern, replacement, out var _);
        }
    }
}
