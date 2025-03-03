using BBTT.CrosswordModel;
using System.Reflection.Emit;

namespace BBTT.CrosswordCore;

public class CrosswordAccesor : ICrosswordAccesor
{
    private ICrosswordGenerator _crosswordGenerator;

    public CrosswordAccesor (ICrosswordGenerator crosswordGenerator)
    {
        _crosswordGenerator = crosswordGenerator;
    }

    public CrosswordGrid AddBlankValuesToGrid (CrosswordGrid result)
    {
        int minRow = result.Grid.Keys.Min(k => k.Item1);
        int maxRow = result.Grid.Keys.Max(k => k.Item1);

        int minCol = result.Grid.Keys.Min(k => k.Item2);
        int maxCol = result.Grid.Keys.Max(k => k.Item2);

        for (int i = minRow; i <= maxRow; i++)
        {
            for (int j = minCol; j <= maxCol; j++)
            {
                if (!result.Grid.ContainsKey((i, j)))
                {
                    result.Grid.Add((i, j), '@');
                }
            }
        }
        return result;

    }

    public async Task<CrosswordGrid> ConstructCrossword (CrosswordWord [] words, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(words);
        var grid = await _crosswordGenerator.ConstructCrossword(words, cancellationToken);
        if (grid != null)
        {
            CrosswordGrid crosswordGrid = new();
            crosswordGrid.Grid = grid;
            return crosswordGrid;
        }
        //TODO Add logic if no grid could be made
        return null;
    }

    public Task<string> ConstructCrossword (CrosswordWord [] verticalWords, CrosswordWord [] horizantalWords, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CrosswordGrid> GetCrosswordGrid (int id)
    {
        throw new NotImplementedException();
    }
}
