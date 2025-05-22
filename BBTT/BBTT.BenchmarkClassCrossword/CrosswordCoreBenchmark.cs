using BBTT.CrosswordAPI.Controllers;
using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using BBTT.Files;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace BBTT.BenchmarkClassCrossword
{
    public class CrosswordCoreBenchmark
    {
        private readonly CrosswordGenerator crosswordGenerator;

        public CrosswordCoreBenchmark()
        {
            crosswordGenerator = new CrosswordGenerator();
        }

        [Benchmark]
        public void TestMethodGenerateCrossword()
        {
            List<CrosswordWord> words = new List<CrosswordWord>()
               {
                   new CrosswordWord("Test", "Test hint", "ACROSS"),
                   new CrosswordWord("Test", "Test hint #2", "DOWN"),
               };

            CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);

            var result = crosswordController.ConstructCrossword(words.ToArray(), CancellationToken.None).Result;
        }
    }
}
