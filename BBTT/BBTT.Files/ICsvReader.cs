using BBTT.CrosswordModel;

namespace BBTT.Files;

public interface ICsvReaderAcessor
{
    Task<IEnumerable<CrosswordWord>> ReadWordsFromCsv (string file);
    Task<IEnumerable<CrosswordWord>> ReadWordsFromCsv (Stream stream);
}