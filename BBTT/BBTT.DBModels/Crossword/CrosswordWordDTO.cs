using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.DBModels.Crossword;
public class CrosswordWordDTO
{
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public int CrosswordId { get; set; }
    public string? Diffuclty { get; set; }
    public string? Language { get; set; }
    public string? Hint { get; set; }

}
