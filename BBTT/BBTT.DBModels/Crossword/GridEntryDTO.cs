using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBTT.DBModels.Crossword;
public class GridEntryDTO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("CrosswordGridId")]
    public int CrosswordGridId { get; set; }
    [Required]
    public int Row { get; set; }
    [Required]
    public int Column { get; set; }
    [MaxLength(1)]
    [Required]
    public char Value { get; set; }
}
