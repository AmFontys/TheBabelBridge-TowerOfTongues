using BBTT.CrosswordModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.CrosswordCore;

public class CrosswordGenerator : ICrosswordGenerator
{
    private Dictionary<(int x, int y), char> grid;

    public async Task<Dictionary<(int x, int y), char>> ConstructCrossword (CrosswordWord [] words, CancellationToken cancellationToken)
    {
        grid = new Dictionary<(int x, int y), char>();
        Random random = new Random();
        words = words.OrderBy(x => random.Next()).ToArray(); // Shuffle the words

        // Place first word in the middle (or a starting point)
        var firstWord = words [ 0 ];
        var x = 0;
        var y = 0;
        PlaceOnGrid(firstWord, x, y);

        // Place the rest of the words
        for (int i = 1; i < words.Length; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            if (!TryPlaceWord(words [ i ]))
            {
                Console.WriteLine($"Could not place word: {words [ i ].Word}");
            }
        }

        return grid;
    }

    private void PlaceOnGrid (CrosswordWord word, int x, int y)
    {
        if (word.Direction == "ACROSS")
        {
            for (int i = 0; i < word.Word.Length; i++)
            {
                grid [ (x + i, y) ] = word.Word [ i ];
            }
        }
        else
        {
            for (int i = 0; i < word.Word.Length; i++)
            {
                grid [ (x, y + i) ] = word.Word [ i ];
            }
        }
    }

    private bool TryPlaceWord (CrosswordWord word)
    {
        List<(int x, int y, int intersectIndex)> intersections = FindIntersections(word);

        if (intersections.Count == 0)
        {
            return false;
        }

        Random random = new Random();
        intersections = intersections.OrderBy(x => random.Next()).ToList();

        foreach (var intersection in intersections)
        {
            int x = intersection.x;
            int y = intersection.y;
            int intersectIndex = intersection.intersectIndex;

            if (word.Direction == "ACROSS")
            {
                x -= intersectIndex;
                if (CanPlaceWord(word, x, y))
                {
                    PlaceOnGrid(word, x, y);
                    return true;
                }
            }
            else
            {
                y -= intersectIndex;
                if (CanPlaceWord(word, x, y))
                {
                    PlaceOnGrid(word, x, y);
                    return true;
                }
            }
        }

        return false;
    }

    private List<(int x, int y, int intersectIndex)> FindIntersections (CrosswordWord word)
    {
        List<(int x, int y, int intersectIndex)> intersections = new List<(int x, int y, int intersectIndex)>();

        for (int i = 0; i < word.Word.Length; i++)
        {
            char currentLetter = word.Word [ i ];

            foreach (var gridCell in grid)
            {
                if (gridCell.Value == currentLetter)
                {
                    intersections.Add((gridCell.Key.x, gridCell.Key.y, i));
                }
            }
        }

        return intersections;
    }

    private bool CanPlaceWord (CrosswordWord word, int x, int y)
    {
        if (word.Direction == "ACROSS")
        {
            for (int i = 0; i < word.Word.Length; i++)
            {
                if (grid.ContainsKey((x + i, y)) && grid [ (x + i, y) ] != word.Word [ i ])
                {
                    return false;
                }
                if (grid.ContainsKey((x + i, y - 1)) || grid.ContainsKey((x + i, y + 1)))
                {
                    if (!grid.ContainsKey((x + i, y)) || grid [ (x + i, y) ] != word.Word [ i ])
                    {
                        return false;
                    }
                }
                if (i > 0 && grid.ContainsKey((x + i - 1, y)) && grid [ (x + i - 1, y) ] != word.Word [ i - 1 ])
                {
                    return false;
                }
                if (i < word.Word.Length - 1 && grid.ContainsKey((x + i + 1, y)) && grid [ (x + i + 1, y) ] != word.Word [ i + 1 ])
                {
                    return false;
                }

            }
        }
        else
        {
            for (int i = 0; i < word.Word.Length; i++)
            {
                if (grid.ContainsKey((x, y + i)) && grid [ (x, y + i) ] != word.Word [ i ])
                {
                    return false;
                }
                if (grid.ContainsKey((x - 1, y + i)) || grid.ContainsKey((x + 1, y + i)))
                {
                    if (!grid.ContainsKey((x, y + i)) || grid [ (x, y + i) ] != word.Word [ i ])
                    {
                        return false;
                    }
                }
                if (i > 0 && grid.ContainsKey((x, y + i - 1)) && grid [ (x, y + i - 1) ] != word.Word [ i - 1 ])
                {
                    return false;
                }
                if (i < word.Word.Length - 1 && grid.ContainsKey((x, y + i + 1)) && grid [ (x, y + i + 1) ] != word.Word [ i + 1 ])
                {
                    return false;
                }

            }
        }

        return true;
    }
}
