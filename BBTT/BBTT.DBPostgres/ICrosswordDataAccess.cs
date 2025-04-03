using BBTT.CrosswordModel;
using BBTT.DBModels.Crossword;

namespace BBTT.DBPostgres;

public interface ICrosswordDataAccess
{
    Task<int> CreateCrossword (Crossword crossword);
    Task<CrosswordDto> GetCrossword (int id);
    Task<CrosswordDto> GetCrossword (string name);
    Task<CrosswordDto> GetCrossword (Crossword crosswordDto);
    Task<int> CreateCrosswordGrid (int id, CrosswordGrid crosswordGrid);
    Task<List<CrosswordDto>> GetCrosswords();
    Crossword MapCrosswords (CrosswordDto crossword);
    List<Crossword> MapCrosswords (List<CrosswordDto> crosswords);
}