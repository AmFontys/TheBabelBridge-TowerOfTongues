using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.DBModels.Crossword;
public class CrosswordDto
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
}
