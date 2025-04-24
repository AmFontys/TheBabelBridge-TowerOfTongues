namespace BBTT.CrosswordModel;

/// <summary>
/// Crossword puzzle model.
/// </summary>
public class Crossword
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public CrosswordGrid? CrosswordGrid { get; set; }

    public List<CrosswordWord> crosswordWords { get; set; } = new List<CrosswordWord>();
}
