using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.DBModels.Crossword;
public class CrosswordDTO
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    [MinLength(1)]
    [Required]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public List<string>? Tags { get; set; }

    [Required]
    public required CrosswordGridDto CrosswordGrid { get; set; }

    [Required]
    public List<CrosswordWordDTO> Words { get; set; } = new List<CrosswordWordDTO>();
}
