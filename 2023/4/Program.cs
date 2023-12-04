using System.Text.RegularExpressions;
using static System.Text.Json.JsonSerializer;

var scratchoffMatches = new List<int>();
using (StreamReader sr = File.OpenText("input/input.txt"))
{
    string s;
    var totalPoints = 0;

    while ((s = sr.ReadLine()) != null)
    {
        var match = Regex.Match(s, @"Card\s+(\d+): ([\d ]+)\| ([\d ]+)");
        var cardNum = match.Groups[1].Value.Trim();
        var winningNumbers = match.Groups[2].Value.Trim().Split(" ").Where(n => !n.Equals(string.Empty)).Select(n => int.Parse(n.Trim())).ToHashSet();
        var myNumbers = match.Groups[3].Value.Trim().Split(" ").Where(n => !n.Equals(string.Empty)).Select(n => int.Parse(n.Trim())).ToHashSet();
        var intersect = winningNumbers.Intersect(myNumbers);
        var numPoints = 0;
        if (intersect.Any())
        {
            numPoints = (int)Math.Pow(2, intersect.Count() - 1);
        }

        totalPoints += numPoints;
        scratchoffMatches.Add(intersect.Count());

        Console.WriteLine($"{cardNum}: {Serialize(winningNumbers)} ... {Serialize(myNumbers)} :: {intersect.Count()}|{numPoints}");
    }

    Console.WriteLine(totalPoints);
    var sum = 0;
    for (int i = 0; i < scratchoffMatches.Count; i++)
    {
        if (scratchoffMatches[i] > 0)
        {
            sum += SumAtDepth(i + 1);
        }
    }

    sum += scratchoffMatches.Count;
    Console.WriteLine(sum);
}

int SumAtDepth(int depth)
{
    if (depth - 1 >= scratchoffMatches.Count)
    {
        return 0;
    }

    var numScratchoffsWon = scratchoffMatches![depth - 1];

    for (int i = 1; i <= scratchoffMatches![depth - 1]; i++)
    {
        numScratchoffsWon += SumAtDepth(depth + i);
    }

    return numScratchoffsWon;
}