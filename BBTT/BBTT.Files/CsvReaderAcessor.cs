using BBTT.CrosswordModel;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace BBTT.Files;

public class CsvReaderAcessor : ICsvReaderAcessor
{

    public async Task<IEnumerable<CrosswordWord>> ReadWordsFromCsv(string file)
    {
        ArgumentNullException.ThrowIfNull(file);

        using var reader = new StreamReader(file);
        using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Read CSV file
        var words = csv.GetRecords<CrosswordWord>();

        return words;
    }

    public async Task<IEnumerable<CrosswordWord>> ReadWordsFromCsv(Stream fileStream)
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound=null
        });

        // Read CSV file
        var words = csv.GetRecords<CrosswordWord>().ToList();

        return await Task.FromResult(words);
    }
}
