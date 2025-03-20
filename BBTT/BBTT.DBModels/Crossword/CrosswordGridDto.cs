using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBTT.DBModels.Crossword;

public class CrosswordGridDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [ForeignKey("CrosswordId")]
    public int CrosswordId { get; set; }
    [Required]
    public List<GridEntryDTO> GridEntries { get; set; } = new List<GridEntryDTO>();

    [NotMapped]
    public Dictionary<(int, int), char> Grid => GridEntries.ToDictionary(e => (e.Row, e.Column), e => e.Value);
}
