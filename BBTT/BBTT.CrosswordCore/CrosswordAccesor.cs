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

    public async Task<string> ConstructCrossword (CrosswordWord [] words, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(words);
        var grid = await _crosswordGenerator.ConstructCrossword(words, cancellationToken);
        if (grid != null)
            return "Something";
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
