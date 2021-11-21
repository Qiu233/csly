
using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

using BenchmarkDotNet.Analysers;


using sly.parser;
using sly.parser.generator;
using bench.json;
using bench.json.model;


namespace bench
{

    [MemoryDiagnoser]
    
    [Config(typeof(Config))]
    public class NoLinqBench
    {


        private class Config : ManualConfig
        {
            public Config()
            {
                var baseJob = Job.MediumRun.With(CsProjCoreToolchain.Current.Value);
                Add(EnvironmentAnalyser.Default);
            }
        }

        
        private Parser<Issue251Parser.Issue251Tokens,Issue251Parser> BenchedParser;

        private string content = "";

        [GlobalSetup]
        public void Setup()
        {
            Console.WriteLine(("SETUP"));
            Console.ReadLine();
            content = ;
            Console.WriteLine("json read.");
            var jsonParser = new EbnfJsonGenericParser();
            var builder = new ParserBuilder<JsonTokenGeneric, JSon>();
            
            var result = builder.BuildParser(jsonParser, ParserType.EBNF_LL_RECURSIVE_DESCENT, "root");
            Console.WriteLine("parser built.");
            if (result.IsError)
            {
                Console.WriteLine("ERROR");
                result.Errors.ForEach(e => Console.WriteLine(e));
            }
            else
            {
                Console.WriteLine("parser ok");
                BenchedParser = result.Result;
            }
            
            Console.WriteLine($"parser {BenchedParser}");
        }

        [Benchmark]
        
        public void TestJson()
        {
            if (BenchedParser == null)
            {
                Console.WriteLine("parser is null");
            }
            else
            {
                var ignored = BenchedParser.Parse(content);    
            }
        }



    }

}
