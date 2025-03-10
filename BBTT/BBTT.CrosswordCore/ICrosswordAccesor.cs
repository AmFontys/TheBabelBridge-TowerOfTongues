﻿using BBTT.CrosswordModel;

namespace BBTT.CrosswordCore;

public interface ICrosswordAccesor
{
    CrosswordGrid AddBlankValuesToGrid (CrosswordGrid result);
    Task<CrosswordGrid> ConstructCrossword (CrosswordWord [] words, CancellationToken cancellationToken);
    Task<string> ConstructCrossword (CrosswordWord [] verticalWords, CrosswordWord [] horizantalWords, CancellationToken cancellationToken);

    Task<CrosswordGrid> GetCrosswordGrid (int id);
}