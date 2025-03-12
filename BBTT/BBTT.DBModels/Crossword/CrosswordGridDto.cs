using System.ComponentModel.DataAnnotations.Schema;

namespace BBTT.DBModels.Crossword;

public class CrosswordGridDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("CrosswordId")]
    public int CrosswordId { get; set; }

    public List<GridEntry> GridEntries { get; set; } = new List<GridEntry>();

    [NotMapped]
    public Dictionary<(int, int), char> Grid => GridEntries.ToDictionary(e => (e.Row, e.Column), e => e.Value);
}
