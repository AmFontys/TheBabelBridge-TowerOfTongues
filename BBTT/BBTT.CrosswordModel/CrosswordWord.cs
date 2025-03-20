using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordModel;
public class CrosswordWord
{
    public CrosswordWord(string word, string language)
    {
        Word = word;
        Language = language;
    }

    public CrosswordWord(string word, string diffuclty, string language, string? hint)
    {
        Word = word;
        Diffuclty = diffuclty;
        Language = language;
        Hint = hint;
    }

    public CrosswordWord(string word, string hint, string direction)
    {
        Word = word;
        Hint = hint;
        Direction = direction.ToUpper();

        Diffuclty = "Basic";
        Language = "En";

    }

    public CrosswordWord()
    {

    }

    public string? Word { get; set; }
    public string? Diffuclty { get; set; }
    public string? Language { get; set; }

    public string? Hint { get; set; }

    public string? Direction { get; set; }

    public override string? ToString()
    {
        return string.Format("{0}, is a {1} word. It comes from {2}", Word, Diffuclty, Language);
    }
}
