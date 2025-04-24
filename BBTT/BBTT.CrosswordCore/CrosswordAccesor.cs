using BBTT.CrosswordModel;
using System.Reflection.Emit;

namespace BBTT.CrosswordCore;

public class CrosswordAccesor : ICrosswordAccesor
{
    private readonly ICrosswordGenerator _crosswordGenerator;

    public CrosswordAccesor(ICrosswordGenerator crosswordGenerator)
    {
        _crosswordGenerator = crosswordGenerator;
    }

    public CrosswordGrid AddBlankValuesToGrid(CrosswordGrid result)
    {
        int minRow = result.GridEntries.Min(k => k.Row);
        int maxRow = result.GridEntries.Max(k => k.Row);

        int minCol = result.GridEntries.Min(k => k.Column);
        int maxCol = result.GridEntries.Max(k => k.Column);

        for (int i = minRow; i <= maxRow; i++)
        {
            for (int j = minCol; j <= maxCol; j++)
            {
                if (!result.GridEntries.Any(e => e.Row == i && e.Column == j))
                {
                    result.GridEntries.Add(new GridEntry { Row = i, Column = j, Value = '@' });
                }
            }
        }
        return result;
    }

    public async Task<CrosswordGrid> ConstructCrossword(CrosswordWord[] words, CancellationToken cancellationToken)
    {
        CrosswordGrid crosswordGrid = new();
        ArgumentNullException.ThrowIfNull(words);
        var grid = await _crosswordGenerator.ConstructCrossword(words, cancellationToken);
        if (grid != null)
        {
            crosswordGrid.GridEntries = grid.Select(kvp => new GridEntry { Row = kvp.Key.x, Column = kvp.Key.y, Value = (char)kvp.Value }).ToList();
            return crosswordGrid;
        }
        //TODO Add logic if no grid could be made
        return crosswordGrid;
    }

    public Task<string> ConstructCrossword(CrosswordWord[] verticalWords, CrosswordWord[] horizantalWords, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CrosswordGrid> GetCrosswordGrid(int id)
    {
        throw new NotImplementedException();
    }
}
