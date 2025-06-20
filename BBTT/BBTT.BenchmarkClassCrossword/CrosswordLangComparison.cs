using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using BenchmarkDotNet.Attributes;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.BenchmarkClassCrossword;
[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class CrosswordLangComparison
{
    private readonly CrosswordGenerator crosswordGenerator;

    [Params(10, 100, 1000, 10000)]
    public int amountOfWords;

    public CrosswordLangComparison ()
    {
        crosswordGenerator = new CrosswordGenerator();
    }

    [Benchmark (Baseline =true)]
    public void TestCsvGenerateCrosswordWithRandomSamplingLatinScript ()
    {
        var csvPath = Path.Combine(AppContext.BaseDirectory, "LangCSV", "Latin.csv");

        try
        {
            using var reader = new StreamReader(csvPath, System.Text.Encoding.UTF8);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csvReader.GetRecords<CrosswordWord>().ToList();

            var random = new Random();
            var sampledRecords = records.OrderBy(_ => random.Next()).Take(amountOfWords).ToList();

            CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);
            var result = crosswordController.ConstructCrossword(sampledRecords.ToArray(), CancellationToken.None).Result;
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

    [Benchmark]
    public void TestCsvGenerateCrosswordWithRandomSamplingBrailleScript ()
    {
        var csvPath = Path.Combine(AppContext.BaseDirectory, "LangCSV", "Braille.csv");
        try
        {
            using var reader = new StreamReader(csvPath, System.Text.Encoding.UTF8);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csvReader.GetRecords<CrosswordWord>().ToList();

            var random = new Random();
            var sampledRecords = records.OrderBy(_ => random.Next()).Take(amountOfWords).ToList();

            CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);
            var result = crosswordController.ConstructCrossword(sampledRecords.ToArray(), CancellationToken.None).Result;
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

    [Benchmark]
    public void TestCsvGenerateCrosswordWithRandomSamplingHiraganaScript ()
    {
        var csvPath = Path.Combine(AppContext.BaseDirectory, "LangCSV", "Hiragana.csv");
        try
        {
            using var reader = new StreamReader(csvPath, System.Text.Encoding.UTF8);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csvReader.GetRecords<CrosswordWord>().ToList();

            var random = new Random();
            var sampledRecords = records.OrderBy(_ => random.Next()).Take(amountOfWords).ToList();

            CrosswordAccesor crosswordController = new CrosswordAccesor(crosswordGenerator);
            var result = crosswordController.ConstructCrossword(sampledRecords.ToArray(), CancellationToken.None).Result;
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
