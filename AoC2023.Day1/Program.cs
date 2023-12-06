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
	var total = 0;
	
	foreach (var line in lines)
	{
		var digits = line
			.Where(c => char.IsDigit(c))
			.Select(c => (int)char.GetNumericValue(c));

		var calibrationValue = digits.First() * 10 + digits.Last();

		total += calibrationValue;
	}

	WriteLine($"Total: {total}");
}

void SolvePartTwo(IEnumerable<string> lines)
{
	var total = 0;

	foreach (var line in lines)
	{
		var replacedValue = line
			.Replace("one", "o1e")
			.Replace("two", "t2o")
			.Replace("three", "t3e")
			.Replace("four", "f4r")
			.Replace("five", "f5e")
			.Replace("six", "s6x")
			.Replace("seven", "s7n")
			.Replace("eight", "e8t")
			.Replace("nine", "n9e");

		var digits = replacedValue
			.Where(c => char.IsDigit(c))
			.Select(c => (int)char.GetNumericValue(c));
			
		var calibrationValue = digits.First() * 10 + digits.Last();

		total += calibrationValue;
	}

	WriteLine($"Total: {total}");
}

