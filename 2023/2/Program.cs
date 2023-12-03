using System.Text.RegularExpressions;
using static System.Text.Json.JsonSerializer;

string[] colors = ["red", "green", "blue"];

using (StreamReader sr = File.OpenText("input/input.txt"))
{
    var gameTotal = 0;
    var powerTotal = 0;
    string s;
    while ((s = sr.ReadLine()) != null)
    {
        var separatorIndex = s.IndexOf(":");
        var gameNumber = int.Parse(s[..separatorIndex].Replace("Game ", ""));

        var gameDetails = s[(separatorIndex + 2)..];

        var knownCubeMaximums = new Dictionary<string,int>() {
            {colors[0], 0},
            {colors[1], 0},
            {colors[2], 0}
        };

        foreach (var color in colors) {
            foreach (Match match in Regex.Matches(gameDetails, $"\\d+(?= {color})").Cast<Match>())
            {
                var numCubes = int.Parse(match.Value);
                if (knownCubeMaximums[color] < numCubes)
                {
                    knownCubeMaximums[color] = numCubes;
                }
            }
        }

        if (knownCubeMaximums[colors[0]] <= 12 && knownCubeMaximums[colors[1]] <= 13 && knownCubeMaximums[colors[2]] <= 14)
        {
            gameTotal += gameNumber;
        }
        
        powerTotal += knownCubeMaximums[colors[0]] * knownCubeMaximums[colors[1]] * knownCubeMaximums[colors[2]];

        Console.WriteLine($"{gameNumber} | {Serialize(knownCubeMaximums)} | {gameTotal}");
    }

    Console.WriteLine($"Game total: {gameTotal}");
    Console.WriteLine($"Sum power of all sets: {powerTotal}");
}