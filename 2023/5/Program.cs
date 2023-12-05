using System.Net;
using static System.Text.Json.JsonSerializer;

var mapInstructions = new Dictionary<string, List<long[]>>();
var seedNumbers = new List<long>();

using (StreamReader sr = File.OpenText("input/input.txt"))
{
    var seedNumberDef = sr.ReadLine().Split(" ")[1..].Select(long.Parse).ToList();
    for (int i = 0; i < seedNumberDef.Count; i+=2)
    {
        for (long seedNum = seedNumberDef[i]; seedNum < seedNumberDef[i] + seedNumberDef[i+1]; seedNum++)
        {
            seedNumbers.Add(seedNum);
        }
    }

    string line;
    string? instructionType = null;
    List<long[]>? instructions = null;

    while ((line = sr.ReadLine()) != null)
    {
        var cleanLine = line.Trim();

        if (cleanLine.Equals(string.Empty))
        {
            if (instructionType != null)
            {
                mapInstructions.Add(instructionType, instructions ?? []);
            }
        }
        else if (cleanLine.Contains("map"))
        {
            instructionType = cleanLine.Replace(":", "");
            instructions = [];
        }
        else
        {
            var mapRange = cleanLine.Split(" ").Select(s => long.Parse(s)).ToArray();
            instructions!.Add(mapRange);
        }
    }
}

Console.WriteLine(Serialize(mapInstructions));

long minLocationNumber = -1;
foreach (var seedNumber in seedNumbers)
{
    long destination = seedNumber;
    foreach (var instructionType in mapInstructions.Keys)
    {
        foreach (var instruction in mapInstructions[instructionType])
        {
            if (destination >= instruction[1] && destination < instruction[1] + instruction[2])
            {
                var offset = destination - instruction[1];
                destination = instruction[0] + offset;
                break;
            }
        }
    }

    minLocationNumber = minLocationNumber == -1 ? destination : Math.Min(minLocationNumber, destination);
}

Console.WriteLine(minLocationNumber);

