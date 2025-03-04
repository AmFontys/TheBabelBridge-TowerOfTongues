using System.ComponentModel.DataAnnotations.Schema;

namespace BBTT.DBModels.Crossword;

public class CrosswordGridDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("CrosswordId")]
    public int CrosswordId { get; set; }

    public Dictionary<(int, int), char> Grid { get; set; } = new Dictionary<(int, int), char>();
}