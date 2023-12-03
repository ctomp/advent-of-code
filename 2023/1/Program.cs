using System.Text.RegularExpressions;

var substitutions = new Dictionary<string,string>() {
    {"oneight", "1eight"},
    {"twone", "2one"},
    {"threeight", "3eight"},
    {"fiveight", "5eight"},
    {"sevenine", "7nine"},
    {"eightwo", "8two"},
    {"eighthree", "8three"},
    {"nineight", "9eight"},
    {"one", "1"},
    {"two", "2"},
    {"three", "3"},
    {"four", "4"},
    {"five", "5"},
    {"six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9"}
};

var totalCalibration = 0;
var counter = 0;
using (StreamReader sr = File.OpenText("input/input.txt"))
{
    string s;
    while ((s = sr.ReadLine()) != null)
    {
        var line = s;
        foreach (var substitution in substitutions.Keys) {
            line = Regex.Replace(line, substitution, substitutions[substitution]);
        }

        var matches = Regex.Matches(line, @"\d", RegexOptions.None);
        var first = matches.First().ToString();
        var last = matches.Last().ToString();
        totalCalibration += Int32.Parse(first + last);

        Console.WriteLine($"{++counter}: {first + last} - {line} - {s}");
    }

    Console.WriteLine(totalCalibration);
}