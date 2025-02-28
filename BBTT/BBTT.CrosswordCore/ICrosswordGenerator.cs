using BBTT.CrosswordModel;

namespace BBTT.CrosswordCore;

public interface ICrosswordGenerator
{
    Task<Dictionary<(int x, int y), char>> ConstructCrossword (CrosswordWord [] words, CancellationToken cancellationToken);
}