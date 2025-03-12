namespace BBTT.DBModels.Crossword;
public class GridEntry
{
    public int Id { get; set; }
    public int CrosswordGridId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public char Value { get; set; }
}
