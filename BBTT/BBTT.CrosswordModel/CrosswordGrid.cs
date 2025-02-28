using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordModel;
public class CrosswordGrid
{
    private Dictionary<Array, char>? grid;

    public Dictionary<Array,char> Grid { get => grid; set => grid = value; }
}
