using System.Text;

using (StreamReader sr = File.OpenText("input/input.txt"))
{
    string s;
    var rows = new List<int[]>();
    var symbolCoords = new List<(int x, int y)>();

    while ((s = sr.ReadLine()) != null)
    {
        var row = new int[s.Length];
        var numberOffset = 0;
        var number = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            var value = s[i];
            
            if (char.IsDigit(value)) {
                number.Append(value);
            } else {
                if (number.Length > 0) {
                    for (int numIndex = numberOffset; numIndex < number.Length + numberOffset; numIndex++)
                    {
                        row[numIndex] = int.Parse(number.ToString());
                    }

                    number = new StringBuilder();
                }

                if (value == '*') {
                    symbolCoords.Add((i, rows.Count));
                }

                numberOffset = i + 1;
            }
        }

        if (number.Length > 0) {
            for (int numIndex = numberOffset; numIndex < number.Length + numberOffset; numIndex++)
            {
                row[numIndex] = int.Parse(number.ToString());
            }
        }

        rows.Add(row);
    }

    var schematicWidth = rows.First().Length;

    int gearRatioTotal = 0;

    foreach (var symbolCoord in symbolCoords)
    {
        int gearRatio = 1;
        int numAdjPartNumbers = 0;

        for (int y = symbolCoord.y - 1; y <= symbolCoord.y + 1; y++)
        {
            int prevNumber = 0;
            
            for (int x = symbolCoord.x - 1; x <= symbolCoord.x + 1; x++)
            {
                if (x > 0 && x < schematicWidth && y >= 0)
                {
                    var numberToAdd = rows[y][x];
                    if (numberToAdd != prevNumber && numberToAdd != 0) 
                    {
                        gearRatio *= numberToAdd;
                        prevNumber = numberToAdd;
                        numAdjPartNumbers++;
                    }
                }
            }
        }

        if (numAdjPartNumbers == 2)
        {
            gearRatioTotal += gearRatio;
        }
    }

    Console.WriteLine(gearRatioTotal);
}