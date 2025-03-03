using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordModel;
public class CrosswordGrid
{
    public Dictionary<(int, int), char> Grid { get; set; } = new Dictionary<(int, int), char>();

    public override string ToString()
    {
        return string.Join(", ", Grid.Select(kv => $"({kv.Key.Item1}, {kv.Key.Item2}): {kv.Value}"));
    }
}
