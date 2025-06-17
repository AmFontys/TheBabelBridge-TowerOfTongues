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
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class CrosswordCoreBenchmark
    {
        private readonly CrosswordGenerator crosswordGenerator;

        [Params(10,100,1000,1000000)]
        public int addWords;

        public CrosswordCoreBenchmark()
        {
            crosswordGenerator = new CrosswordGenerator();
        }

        [Benchmark]
        public void TestMethodGenerateCrossword ()
        {
            List<CrosswordWord> words = new List<CrosswordWord>();                       

            // Add more words as needed
            for(int i =0; i < addWords; i++)
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

                var records = csvReader.GetRecords<CrosswordWord>().ToList();
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
