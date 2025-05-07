using BBTT.CrosswordModel;
using BBTT.DBModels.Crossword;

namespace BBTT.DBPostgres;

public interface ICrosswordDataAccess
{
    Task<int> CreateCrossword (Crossword crossword);
    Task<CrosswordDTO> GetCrossword (int id);
    Task<CrosswordDTO> GetCrossword (string name);
    Task<CrosswordDTO> GetCrossword (Crossword crosswordDto);
    Task<int> CreateCrosswordGrid (int id, CrosswordGrid crosswordGrid);
    Task<List<CrosswordDTO>> GetCrosswords();
    Crossword MapCrosswords (CrosswordDTO crossword);
    List<Crossword> MapCrosswords (List<CrosswordDTO> crosswords);
}