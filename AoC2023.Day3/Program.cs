using static System.Console;
using Position = (int i, int j);

var filename = args[0];
var part = args[1];

if (!File.Exists(filename))
    throw new InvalidOperationException("File not found.");

var input = await File.ReadAllLinesAsync(filename);

if (part == "1")
    SolvePartOne(input);
else if (part == "2")
    SolvePartTwo(input);
else
    throw new InvalidOperationException("Part not found.");

return;

void SolvePartOne(string[] lines)
{
    var total = 0;

    for (var i = 0; i < lines.Length; i++)
    {
        for (var j = 0; j < lines[i].Length; j++)
        {
            var foundPart = false;
            var number = "";

            while (j < lines[i].Length && char.IsDigit(lines[i][j]))
            {
                number += lines[i][j];

                if (!foundPart)
                    foundPart = SearchForPredicateAround(lines, i, j, CheckForPart) != null;

                j += 1;
            }

            if (number == "" || !foundPart)
                continue;

            total += int.Parse(number);
        }
    }

    WriteLine($"Total: {total}");
}

bool CheckForPart(char value) => value != '.' && !char.IsDigit(value);

Position? SearchForPredicateAround(string[] lines, int i, int j, Predicate<char> predicate)
{
    if (i > 0)
    {
        var lineAbove = lines[i - 1];

        if (j > 0 && predicate(lineAbove[j - 1]))
            return (i - 1, j - 1);

        if (predicate(lineAbove[j]))
            return (i - 1, j);

        if (j < lineAbove.Length - 1 && predicate(lineAbove[j + 1]))
            return (i - 1, j + 1);
    }

    if (j > 0 && predicate(lines[i][j - 1]))
        return (i, j - 1);

    if (j < lines[i].Length - 1 && predicate(lines[i][j + 1]))
        return (i, j + 1);

    if (i < lines.Length - 1)
    {
        var lineBelow = lines[i + 1];

        if (j > 0 && predicate(lineBelow[j - 1]))
            return (i + 1, j - 1);

        if (predicate(lineBelow[j]))
            return (i + 1, j);

        if (j < lineBelow.Length - 1 && predicate(lineBelow[j + 1]))
            return (i + 1, j + 1);
    }

    return null;
}

bool CheckForGear(char value) => value == '*';

void SolvePartTwo(string[] lines)
{
    var valuesByGear = new Dictionary<(int i, int j), List<int>>();

    for (var i = 0; i < lines.Length; i++)
    {
        for (var j = 0; j < lines[i].Length; j++)
        {
            Position? gearPosition = null;
            var number = "";

            while (j < lines[i].Length && char.IsDigit(lines[i][j]))
            {
                number += lines[i][j];

                if (gearPosition is null)
                    gearPosition = SearchForPredicateAround(lines, i, j, CheckForGear);

                j += 1;
            }

            if (number == "" || gearPosition is null)
                continue;

            valuesByGear.TryAdd(gearPosition.Value, new List<int>());

            valuesByGear[gearPosition.Value].Add(int.Parse(number));
        }
    }

    var total = valuesByGear
        .Where(v => v.Value.Count > 1)
        .Select((v) => v.Value.Aggregate<int, int>(1, (acc, v) => acc * v))
        .Sum();

    WriteLine($"Total: {total}");
}
