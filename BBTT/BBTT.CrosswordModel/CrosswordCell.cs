using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordModel;
public class CrosswordCell
{
    public char Character { get; set; }
    public CrosswordWord Word { get; set; } // Reference to the word it belongs to
}
