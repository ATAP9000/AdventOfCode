using System.Reflection;

namespace AdventOfCodeDay2
{
    internal class Program
    {
        const string TheSoCalledDocument = "AdventOfCodeDay2Gems.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2023 - Day 2: Gem Games");
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
            FirstPart(documentPath);
            SecondPart(documentPath);
        }

        public static void SecondPart(string documentPath)
        {
            string[] lines = File.ReadAllLines(documentPath);
            List<int> totalValuePerGame = [];
            for (int i = 0; i < lines.Length; i++)
            {
                string auxLine = lines[i].Trim().Replace(';', ',');
                string[] gameValues = auxLine.Split(':');
                string[] gemsShown = gameValues[1].Split(",");
                int currentMaximumRed = 0;
                int currentMaximumBlue = 0;
                int currentMaximumGreen = 0;
                foreach (var gems in gemsShown)
                {
                    string auxGems = gems.Trim();
                    string[] values = auxGems.Split(" ");
                    short amountOfGems = short.Parse(values[0]);
                    string colorOfGem = values[1];
                    switch (colorOfGem)
                    {
                        case "red":
                            if (currentMaximumRed == 0 || amountOfGems > currentMaximumRed)
                                currentMaximumRed = amountOfGems;
                            break;
                        case "blue":
                            if (currentMaximumBlue == 0 || amountOfGems > currentMaximumBlue)
                                currentMaximumBlue = amountOfGems;
                            break;
                        case "green":
                            if (currentMaximumGreen == 0 || amountOfGems > currentMaximumGreen)
                                currentMaximumGreen = amountOfGems;
                            break;
                        default:
                            break;
                    }
                }
                int sumOfGems = currentMaximumRed * currentMaximumBlue * currentMaximumGreen;
                totalValuePerGame.Add(sumOfGems);
            }
            Console.WriteLine("Total Sum Part 2:" + totalValuePerGame.Sum());
        }

        public static void FirstPart(string documentPath)
        {
            string[] lines = File.ReadAllLines(documentPath);
            List<int> possibleGames = Enumerable.Range(1, lines.Length).ToList();
            List<int> impossibleGames = [];
            for (int i = 0; i < lines.Length; i++)
            {
                bool isGameImpossible = false;
                string auxLine = lines[i].Trim().Replace(';', ',');
                string[] gameValues = auxLine.Split(':');
                string[] gemsShown = gameValues[1].Split(",");
                foreach (var gems in gemsShown)
                {
                    string auxGems = gems.Trim();
                    string[] values = auxGems.Split(" ");
                    short amountOfGems = short.Parse(values[0]);
                    string colorOfGem = values[1];
                    if (amountOfGems > 12 && colorOfGem == "red")
                    {
                        isGameImpossible = true;
                        break;
                    }
                    if (amountOfGems > 13 && colorOfGem == "green")
                    {
                        isGameImpossible = true;
                        break;
                    }
                    if (amountOfGems > 14 && colorOfGem == "blue")
                    {
                        isGameImpossible = true;
                        break;
                    }
                }
                if (isGameImpossible)
                    impossibleGames.Add(i + 1);
            }
            Console.WriteLine("Total Sum based on possible games " + possibleGames.Except(impossibleGames).Sum());
        }
    }
}
