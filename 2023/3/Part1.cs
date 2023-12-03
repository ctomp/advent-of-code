// using System.Text;
// using static System.Text.Json.JsonSerializer;

// using (StreamReader sr = File.OpenText("input/input.txt"))
// {
//     string s;
//     var rows = new List<int[]>();
//     var symbolCoords = new List<(int x, int y)>();

//     while ((s = sr.ReadLine()) != null)
//     {
//         var row = new int[s.Length];
//         var numberOffset = 0;
//         var number = new StringBuilder();
//         for (int i = 0; i < s.Length; i++)
//         {
//             var value = s[i];
            
//             if (char.IsDigit(value)) {
//                 number.Append(value);
//             } else {
//                 if (number.Length > 0) {
//                     for (int numIndex = numberOffset; numIndex < number.Length + numberOffset; numIndex++)
//                     {
//                         row[numIndex] = int.Parse(number.ToString());
//                     }

//                     number = new StringBuilder();
//                 }

//                 if (value != '.') {
//                     symbolCoords.Add((i, rows.Count));
//                 }

//                 numberOffset = i + 1;
//             }
//         }

//         if (number.Length > 0) {
//             for (int numIndex = numberOffset; numIndex < number.Length + numberOffset; numIndex++)
//             {
//                 row[numIndex] = int.Parse(number.ToString());
//             }
//         }

//         rows.Add(row);
//         Console.WriteLine($"{Serialize(row)}");
//     }

//     var schematicWidth = rows.First().Length;

//     int sum = 0;
//     foreach (var symbolCoord in symbolCoords)
//     {
//         int prevNumber = 0;
//         for (int x = symbolCoord.x - 1; x <= symbolCoord.x + 1; x++)
//         {
//             if (x > 0 && x < schematicWidth && symbolCoord.y - 1 >= 0)
//             {
//                 var numberToAdd = rows[symbolCoord.y - 1][x];
//                 if (numberToAdd != prevNumber && numberToAdd != 0) 
//                 {
//                     sum += numberToAdd;
//                     prevNumber = numberToAdd;
//                     Console.WriteLine($"Adding {numberToAdd}");
//                 }
//             }
//         }

//         prevNumber = 0;

//         for (int x = symbolCoord.x - 1; x <= symbolCoord.x + 1; x++)
//         {
//             if (x > 0 && x < schematicWidth && symbolCoord.y >= 0)
//             {
//                 var numberToAdd = rows[symbolCoord.y][x];
//                 if (numberToAdd != prevNumber && numberToAdd != 0) 
//                 {
//                     sum += numberToAdd;
//                     prevNumber = numberToAdd;
//                     Console.WriteLine($"Adding {numberToAdd}");
//                 }
//             }
//         }

//         prevNumber = 0;

//         for (int x = symbolCoord.x - 1; x <= symbolCoord.x + 1; x++)
//         {
//             if (x > 0 && x < schematicWidth && symbolCoord.y + 1 >= 0)
//             {
//                 var numberToAdd = rows[symbolCoord.y + 1][x];
//                 if (numberToAdd != prevNumber && numberToAdd != 0) 
//                 {
//                     sum += numberToAdd;
//                     prevNumber = numberToAdd;
//                     Console.WriteLine($"Adding {numberToAdd}");
//                 }
//             }
//         }
//     }

//     Console.WriteLine(sum);
// }