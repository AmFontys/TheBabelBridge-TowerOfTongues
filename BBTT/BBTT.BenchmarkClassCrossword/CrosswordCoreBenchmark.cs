using BBTT.CrosswordAPI.Controllers;
using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using BBTT.Files;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;

namespace BBTT.BenchmarkClassCrossword
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MarkdownExporter, CsvExporter, HtmlExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class CrosswordCoreBenchmark
    {
        private readonly CrosswordGenerator crosswordGenerator;

        [Params(10,100,1000,10000)]
        public int amountOfWords;

        public CrosswordCoreBenchmark()
        {
            crosswordGenerator = new CrosswordGenerator();
        }

        [Benchmark(Baseline =true)]
        public void TestMethodGenerateCrossword ()
        {
            List<CrosswordWord> words = new List<CrosswordWord>();                       

            // Add more words as needed
            for(int i =0; i < amountOfWords; i++)
            {
                words.Add(new CrosswordWord($"Test{i}", $"Test hint #{i}", "ACROSS"));
            }

            CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);

            var result = crosswordController.ConstructCrossword(words.ToArray(), CancellationToken.None).Result;
        }

        [Benchmark]
        public void TestCsvGenerateCrossword()
        {
            var csvPath = Path.Combine(AppContext.BaseDirectory, "testCSV.csv");
            try
            {
                using var reader = new StreamReader(csvPath, System.Text.Encoding.UTF8);
                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                });

                var records = csvReader.GetRecords<CrosswordWord>().Take(amountOfWords).ToList();
                CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);
                var result = crosswordController.ConstructCrossword(records.ToArray(), CancellationToken.None).Result;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"CSV file not found: {ex.Message}");
                throw;
            }            
            catch (IOException ex)
            {
                Console.WriteLine($"IO error while accessing CSV file: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

    }
}
