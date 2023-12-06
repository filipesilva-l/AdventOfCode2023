using static System.Console;

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

void SolvePartOne(IEnumerable<string> lines)
{
    const int redLimit = 12;
    const int greenLimit = 13;
    const int blueLimit = 14;

    var total = 0;

    foreach (var line in lines)
    {
        var gameParts = line.Split(": ");

        var id = int.Parse(gameParts[0]);

        var groupedValues = gameParts[1].Split(';').Select(p => new GroupValue(p)).ToList();

        var redInvalid = groupedValues.Any(g => g.Red > redLimit);
        var greenInvalid = groupedValues.Any(g => g.Green > greenLimit);
        var blueInvalid = groupedValues.Any(g => g.Blue > blueLimit);

        var invalid = redInvalid || greenInvalid || blueInvalid;

        if (!invalid)
            total += id;
    }

    WriteLine($"Total: {total}");
}

void SolvePartTwo(IEnumerable<string> lines)
{
    var total = 0;

    foreach (var line in lines)
    {
        var groupedValues = line.Split(": ")[1].Split(';').Select(p => new GroupValue(p)).ToList();

        var maxRed = groupedValues.Select(g => g.Red).Max();
        var maxGreen = groupedValues.Select(g => g.Green).Max();
        var maxBlue = groupedValues.Select(g => g.Blue).Max();

        var power = maxRed * maxGreen * maxBlue;

        total += power;
    }

    WriteLine($"Total: {total}");
}

internal record GroupValue(int Red, int Green, int Blue)
{
    public GroupValue(string part)
        : this(0, 0, 0)
    {
        var splittedPart = part.Split(',');

        foreach (var val in splittedPart)
        {
            var splitted = val.Trim().Split(' ');

            var number = int.Parse(splitted[0]);
            var color = splitted[1];

            switch (color)
            {
                case "red":
                {
                    Red += number;
                    break;
                }
                case "green":
                {
                    Green += number;
                    break;
                }
                case "blue":
                {
                    Blue += number;
                    break;
                }
            }
        }
    }
}
