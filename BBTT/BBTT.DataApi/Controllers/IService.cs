﻿
using BBTT.DBModels.Crossword;

namespace BBTT.DataApi.Controllers;

public interface IService
{
    Task<IEnumerable<CrosswordDTO>> GetCrossword ();
}