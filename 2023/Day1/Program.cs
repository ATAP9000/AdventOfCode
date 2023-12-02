using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Day1
{
    internal class Program
    {
        const string TheSoCalledDocument = "AdventOfCode2023Day1.txt";

        enum Numbers
        {
            zero = 0,
            one = 1,
            two = 2,
            three = 3,
            four = 4,
            five = 5,
            six = 6,
            seven = 7,
            eight = 8,
            nine = 9
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2023 - Day 1: Calibration Document");
            string? currentExecutingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(currentExecutingPath))
            {
                Console.WriteLine("What the spruce?\n where is the current executing path then!?\n bruh i dont know lol.");
                return;
            }

            string documentPath = Path.Combine(currentExecutingPath!, TheSoCalledDocument);
            if (!File.Exists(documentPath))
            {
                Console.WriteLine("Where is the document HUH!?\n PUT IT OR ELSE...\n i will cry about it k.\n shutting down btw.");
                return;
            }

            string[] lines = File.ReadAllLines(documentPath);
            Regex rx = new(@"(zero|one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            int result = 0;

            StringBuilder stringBuilder = new();
            foreach (var line in lines)
            {
                bool firstHasBeenFound = false;
                string auxLine = BigBrainCleaning(line);
                MatchCollection matches = rx.Matches(auxLine.Trim());
                foreach (Match match in matches)
                {
                    if (!firstHasBeenFound)
                    {
                        auxLine = auxLine.Replace(match.Value, ((int)Enum.Parse(typeof(Numbers), match.Value, true)).ToString());
                        firstHasBeenFound = true;
                    }
                    else
                    {
                        auxLine = auxLine.Replace(matches[^1].Value, ((int)Enum.Parse(typeof(Numbers), matches[^1].Value, true)).ToString());
                        break;
                    }
                }
                string first = auxLine.First(finalDigit => char.IsDigit(finalDigit)).ToString();
                string last = auxLine.Last(finalDigit => char.IsDigit(finalDigit)).ToString();
                result += int.Parse(first + last);
                stringBuilder.Append(auxLine + "\n");
            }
            Console.WriteLine(stringBuilder.ToString());
            Console.WriteLine("Required Calibration: " + result);
        }

        public static string BigBrainCleaning(string line)
        {
            Regex rx = new(@"(oneight|twone|threeight|sevenine|eightwo|eighthree|nineight)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(line.Trim());
            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "oneight":
                        line = line.Replace(match.Value, "oneeight");
                        break;
                    case "twone":
                        line = line.Replace(match.Value, "twoone");
                        break;
                    case "threeight":
                        line = line.Replace(match.Value, "threeeight");
                        break;
                    case "sevenine":
                        line = line.Replace(match.Value, "sevennine");
                        break;
                    case "eightwo":
                        line = line.Replace(match.Value, "eighttwo");
                        break;
                    case "eighthree":
                        line = line.Replace(match.Value, "eightthree");
                        break;
                    case "nineight":
                        line = line.Replace(match.Value, "nineeight");
                        break;
                    default:
                        break;
                }
            }
            return line;
        }
    }
}
