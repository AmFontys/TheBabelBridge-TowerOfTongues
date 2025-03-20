using BBTT.CrosswordModel;
using BBTT.DBModels.Crossword;

namespace BBTT.DBPostgres;

public interface ICrosswordDataAccess
{
    Task CreateCrossword (Crossword crossword);
    Task<CrosswordDto> GetCrossword (int id);
    Task<CrosswordDto> GetCrossword (Crossword crosswordDto);
    Task CreateCrosswordGrid (Task<CrosswordDto> result, CrosswordGrid crosswordGrid);


}