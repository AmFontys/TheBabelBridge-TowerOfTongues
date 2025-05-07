using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BBTT.DBModels.Crossword;

public class CrosswordGridDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public int CrosswordId { get; set; }

    [ForeignKey("CrosswordId")]
    [JsonIgnore]
    public CrosswordDTO Crossword { get; set; }

    [Required]
    public List<GridEntryDTO> GridEntries { get; set; } = new List<GridEntryDTO>();

    [NotMapped]
    [JsonIgnore]
    public Dictionary<(int, int), char> Grid => GridEntries.ToDictionary(e => (e.Row, e.Column), e => e.Value);
}
