using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordModel;
public class CrosswordGrid
{
    public List<GridEntry> GridEntries { get; set; } = new List<GridEntry>();

    public override string ToString()
    {
        return string.Join(", ", GridEntries.Select(e => $"({e.Row}, {e.Column}): {e.Value}"));
    }
}
