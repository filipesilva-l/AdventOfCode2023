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

void SolvePartOne(string[] lines)
{
    var total = 0;

    foreach (var line in lines)
    {
        var count = GetWinningCount(line);

        total += count switch
        {
            1 => 1,
            _ => (int)Math.Pow(2, count - 1)
        };
    }

    WriteLine($"Total: {total}");
}

void SolvePartTwo(string[] lines)
{
    var cache = new Dictionary<string, int>();

    var total = 0;

    foreach (var line in lines)
    {
        total += GetTotalCount(lines, line, cache);
    }

    WriteLine($"Total: {total}");
}

int GetTotalCount(string[] lines, string line, Dictionary<string, int> cache)
{
    if (cache.TryGetValue(line, out var total))
        return total;

    var count = GetWinningCount(line);
    var index = Array.IndexOf(lines, line);

    if (count == 0)
        return 1;

    total = 1 + lines.Skip(index + 1).Take(count).Select(l => GetTotalCount(lines, l, cache)).Sum();

    cache.TryAdd(line, total);

    return total;
}

int GetWinningCount(string line)
{
    var content = line.Split(':')[1]
        .Split('|')
        .Select(
            part =>
                part.Trim()
                    .Split(' ')
                    .Where(v => !string.IsNullOrEmpty(v))
                    .Select(v => int.Parse(v.Trim()))
                    .ToList()
        )
        .ToList();

    var winning = content.First();
    var mine = content.Last();

    return winning.Intersect(mine).Count();
}
