using System.Reflection;

namespace AdventOfCode2023Day3
{
    internal class Program
    {
        const string TheSoCalledDocument = "AdventOfCodeDay3.txt";
        public static List<string> alreadyAdded = [];

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

        public static void FirstPart(string documentPath)
        {
            char[,] charGrid = ToGrid(documentPath);

            int maxX = charGrid.GetLength(0);
            int maxY = charGrid.Length;
            int result = 0;

            for (int i = 0; i < charGrid.GetLength(0); i++)
            {
                for (int j = 0; j < charGrid.GetLength(1); j++)
                {
                    if (!char.IsDigit(charGrid[i, j]) && charGrid[i, j] != '.')
                    {
                        bool ignorePosition2, ignorePosition3, ignorePosition7, ignorePosition8;
                        ignorePosition2 = ignorePosition3 = ignorePosition7 = ignorePosition8 = false;

                        if (i == 0 || j == 0 || i == maxX || j == maxY)
                            continue;

                        // get top left
                        if (char.IsDigit(charGrid[i - 1, j - 1]))
                        {
                            ignorePosition2 = true;
                            int amountOnLeft = DigitsOnSide(charGrid, j - 1, i - 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i - 1},{j - 3};{i - 1},{j - 2};{i - 1},{j - 1}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i - 1, j - 3].ToString() + charGrid[i - 1, j - 2].ToString() + charGrid[i - 1, j - 1]).ToString());
                                    alreadyAdded.Add($"{i - 1},{j - 1}");
                                    alreadyAdded.Add($"{i - 1},{j - 2}");
                                    alreadyAdded.Add($"{i - 1},{j - 3}");
                                }
                                else
                                    ignorePosition2 = false;
                            }
                            else if (amountOnLeft == 1)
                            {
                                if (char.IsDigit(charGrid[i - 1, j]))
                                {
                                    ignorePosition3 = true;
                                    string toFind = $"{i - 1},{j - 2};{i - 1},{j - 1};{i - 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 2].ToString() + charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 2}");
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                        alreadyAdded.Add($"{i - 1},{j}");
                                    }
                                    else
                                    {
                                        ignorePosition2 = false;
                                        ignorePosition3 = false;
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j - 2};{i - 1},{j - 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 2].ToString() + charGrid[i - 1, j - 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 2}");
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                    }
                                    else
                                        ignorePosition2 = false;
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j - 1, i - 1, false);
                                if (amountOnRight == 2)
                                {
                                    ignorePosition3 = true;
                                    string toFind = $"{i - 1},{j - 1};{i - 1},{j};{i - 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                    }
                                    else
                                    {
                                        ignorePosition2 = false;
                                        ignorePosition3 = false;
                                    }
                                }
                                else if (amountOnRight == 1)
                                {
                                    ignorePosition3 = true;
                                    string toFind = $"{i - 1},{j - 1};{i - 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                    }
                                    else
                                    {
                                        ignorePosition2 = false;
                                        ignorePosition3 = false;
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j - 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                    }
                                    else
                                        ignorePosition2 = false;
                                }
                            }
                        }

                        // get top middle
                        if (char.IsDigit(charGrid[i - 1, j]) && !ignorePosition2)
                        {
                            ignorePosition3 = true;
                            int amountOnLeft = DigitsOnSide(charGrid, j, i - 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i - 1},{j - 2};{i - 1},{j - 1};{i - 1},{j}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i - 1, j - 2].ToString() + charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j]).ToString());
                                    alreadyAdded.Add($"{i - 1},{j - 2}");
                                    alreadyAdded.Add($"{i - 1},{j - 1}");
                                    alreadyAdded.Add($"{i - 1},{j}");
                                }
                                else
                                    ignorePosition3 = false;
                            }
                            else if (amountOnLeft == 1)
                            {
                                // check right then
                                if (char.IsDigit(charGrid[i - 1, j + 1]))
                                {
                                    string toFind = $"{i - 1},{j - 1};{i - 1},{j};{i - 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                    }
                                    else
                                        ignorePosition3 = false;
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j - 1};{i - 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j - 1}");
                                        alreadyAdded.Add($"{i - 1},{j}");
                                    }
                                    else
                                        ignorePosition3 = false;
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j, i - 1, false);
                                if (amountOnRight == 2)
                                {
                                    string toFind = $"{i - 1},{j};{i - 1},{j + 1};{i - 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1].ToString() + charGrid[i - 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                        alreadyAdded.Add($"{i - 1},{j + 2}");
                                    }
                                    else
                                        ignorePosition3 = false;
                                }
                                else if (amountOnRight == 1)
                                {
                                    string toFind = $"{i - 1},{j};{i - 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                    }
                                    else
                                        ignorePosition3 = false;
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                    }
                                    else
                                        ignorePosition3 = false;
                                }
                            }
                        }

                        // get top right
                        if (char.IsDigit(charGrid[i - 1, j + 1]) && !ignorePosition3)
                        {
                            int amountOnLeft = DigitsOnSide(charGrid, j + 1, i - 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i - 1},{j - 1};{i - 1},{j};{i - 1},{j + 1}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i - 1, j - 1].ToString() + charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1]).ToString());
                                    alreadyAdded.Add($"{i - 1},{j - 1}");
                                    alreadyAdded.Add($"{i - 1},{j}");
                                    alreadyAdded.Add($"{i - 1},{j + 1}");
                                }
                            }
                            else if (amountOnLeft == 1)
                            {
                                if (char.IsDigit(charGrid[i - 1, j + 2]))
                                {
                                    string toFind = $"{i - 1},{j};{i - 1},{j + 1};{i - 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1].ToString() + charGrid[i - 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                        alreadyAdded.Add($"{i - 1},{j + 2}");
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j};{i - 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j].ToString() + charGrid[i - 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j}");
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                    }
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j + 1, i - 1, false);
                                if (amountOnRight == 2)
                                {
                                    string toFind = $"{i - 1},{j + 1};{i - 1},{j + 2};{i - 1},{j + 3}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j + 1].ToString() + charGrid[i - 1, j + 2].ToString() + charGrid[i - 1, j + 3]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                        alreadyAdded.Add($"{i - 1},{j + 2}");
                                        alreadyAdded.Add($"{i - 1},{j + 3}");
                                    }
                                }
                                else if (amountOnRight == 1)
                                {
                                    string toFind = $"{i - 1},{j + 1};{i - 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j + 1].ToString() + charGrid[i - 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                        alreadyAdded.Add($"{i - 1},{j + 2}");
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i - 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i - 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i - 1},{j + 1}");
                                    }
                                }
                            }
                        }

                        // get middle left
                        if (char.IsDigit(charGrid[i, j - 1]))
                        {
                            int amountOnLeft = DigitsOnSide(charGrid, j - 1, i);
                            if (amountOnLeft == 2)
                            {
                                result += int.Parse((charGrid[i, j - 3].ToString() + charGrid[i, j - 2].ToString() + charGrid[i, j - 1]).ToString());
                                alreadyAdded.Add($"{i},{j - 3}");
                                alreadyAdded.Add($"{i},{j - 2}");
                                alreadyAdded.Add($"{i},{j - 1}");
                            }
                            else if (amountOnLeft == 1)
                            {
                                result += int.Parse((charGrid[i, j - 2].ToString() + charGrid[i, j - 1]).ToString());
                                alreadyAdded.Add($"{i},{j - 2}");
                                alreadyAdded.Add($"{i},{j - 1}");
                            }
                            else
                            {
                                result += int.Parse((charGrid[i, j - 1]).ToString());
                                alreadyAdded.Add($"{i},{j - 1}");
                            }
                        }

                        // get middle right
                        if (char.IsDigit(charGrid[i, j + 1]))
                        {
                            int amountOnRight = DigitsOnSide(charGrid, j + 1, i, false);
                            if (amountOnRight == 2)
                            {
                                result += int.Parse((charGrid[i, j + 1].ToString() + charGrid[i, j + 2].ToString() + charGrid[i, j + 3]).ToString());
                                alreadyAdded.Add($"{i},{j + 1}");
                                alreadyAdded.Add($"{i},{j + 2}");
                                alreadyAdded.Add($"{i},{j + 3}");
                            }
                            else if (amountOnRight == 1)
                            {
                                result += int.Parse((charGrid[i, j + 1].ToString() + charGrid[i, j + 2]).ToString());
                                alreadyAdded.Add($"{i},{j + 1}");
                                alreadyAdded.Add($"{i},{j + 2}");
                            }
                            else
                            {
                                result += int.Parse((charGrid[i, j + 1]).ToString());
                                alreadyAdded.Add($"{i},{j + 1}");
                            }
                        }

                        // get bottom left
                        if (char.IsDigit(charGrid[i + 1, j - 1]))
                        {
                            ignorePosition7 = true;
                            int amountOnLeft = DigitsOnSide(charGrid, j - 1, i + 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i + 1},{j - 3};{i + 1},{j - 2};{i + 1},{j - 1}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i + 1, j - 3].ToString() + charGrid[i + 1, j - 2].ToString() + charGrid[i + 1, j - 1]).ToString());
                                    alreadyAdded.Add($"{i + 1},{j - 1}");
                                    alreadyAdded.Add($"{i + 1},{j - 2}");
                                    alreadyAdded.Add($"{i + 1},{j - 3}");
                                }
                                else
                                    ignorePosition7 = false;
                            }
                            else if (amountOnLeft == 1)
                            {
                                if (char.IsDigit(charGrid[i + 1, j]))
                                {
                                    ignorePosition8 = true;
                                    string toFind = $"{i + 1},{j - 2};{i + 1},{j - 1};{i + 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 2].ToString() + charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 2}");
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                        alreadyAdded.Add($"{i + 1},{j}");
                                    }
                                    else
                                    {
                                        ignorePosition7 = false;
                                        ignorePosition8 = false;
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j - 2};{i + 1},{j - 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 2].ToString() + charGrid[i + 1, j - 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 2}");
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                    }
                                    else
                                        ignorePosition7 = false;
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j - 1, i + 1, false);
                                if (amountOnRight == 2)
                                {
                                    ignorePosition8 = true;
                                    string toFind = $"{i + 1},{j - 1};{i + 1},{j};{i + 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                    }
                                    else
                                    {
                                        ignorePosition7 = false;
                                        ignorePosition8 = false;
                                    }
                                }
                                else if (amountOnRight == 1)
                                {
                                    ignorePosition8 = true;
                                    string toFind = $"{i + 1},{j - 1};{i + 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                    }
                                    else
                                    {
                                        ignorePosition7 = false;
                                        ignorePosition8 = false;
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j - 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                    }
                                    else
                                        ignorePosition7 = false;
                                }
                            }
                        }

                        // get bottom middle
                        if (char.IsDigit(charGrid[i + 1, j]) && !ignorePosition7)
                        {
                            ignorePosition8 = true;
                            int amountOnLeft = DigitsOnSide(charGrid, j, i + 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i + 1},{j - 2};{i + 1},{j - 1};{i + 1},{j}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i + 1, j - 2].ToString() + charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j]).ToString());
                                    alreadyAdded.Add($"{i + 1},{j - 2}");
                                    alreadyAdded.Add($"{i + 1},{j - 1}");
                                    alreadyAdded.Add($"{i + 1},{j}");
                                }
                                else
                                    ignorePosition8 = false;
                            }
                            else if (amountOnLeft == 1)
                            {
                                // check right then
                                if (char.IsDigit(charGrid[i + 1, j + 1]))
                                {
                                    string toFind = $"{i + 1},{j - 1};{i + 1},{j};{i + 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                    }
                                    else
                                        ignorePosition8 = false;
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j - 1};{i + 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j - 1}");
                                        alreadyAdded.Add($"{i + 1},{j}");
                                    }
                                    else
                                        ignorePosition8 = false;
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j, i + 1, false);
                                if (amountOnRight == 2)
                                {
                                    string toFind = $"{i + 1},{j};{i + 1},{j + 1};{i + 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1].ToString() + charGrid[i + 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                        alreadyAdded.Add($"{i + 1},{j + 2}");
                                    }
                                    else
                                        ignorePosition8 = false;
                                }
                                else if (amountOnRight == 1)
                                {
                                    string toFind = $"{i + 1},{j};{i + 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                    }
                                    else
                                        ignorePosition8 = false;
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                    }
                                    else
                                        ignorePosition8 = false;
                                }
                            }
                        }

                        // get bottom right
                        if (char.IsDigit(charGrid[i + 1, j + 1]) && !ignorePosition8)
                        {
                            int amountOnLeft = DigitsOnSide(charGrid, j + 1, i + 1);
                            if (amountOnLeft == 2)
                            {
                                string toFind = $"{i + 1},{j - 1};{i + 1},{j};{i + 1},{j + 1}";
                                if (!AlreadyOnList(toFind))
                                {
                                    result += int.Parse((charGrid[i + 1, j - 1].ToString() + charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1]).ToString());
                                    alreadyAdded.Add($"{i + 1},{j - 1}");
                                    alreadyAdded.Add($"{i + 1},{j}");
                                    alreadyAdded.Add($"{i + 1},{j + 1}");
                                }
                            }
                            else if (amountOnLeft == 1)
                            {
                                if (char.IsDigit(charGrid[i + 1, j + 2]))
                                {
                                    string toFind = $"{i + 1},{j};{i + 1},{j + 1};{i + 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1].ToString() + charGrid[i + 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                        alreadyAdded.Add($"{i + 1},{j + 2}");
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j};{i + 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j].ToString() + charGrid[i + 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j}");
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                    }
                                }
                            }
                            else
                            {
                                int amountOnRight = DigitsOnSide(charGrid, j + 1, i + 1, false);
                                if (amountOnRight == 2)
                                {
                                    string toFind = $"{i + 1},{j + 1};{i + 1},{j + 2};{i + 1},{j + 3}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j + 1].ToString() + charGrid[i + 1, j + 2].ToString() + charGrid[i + 1, j + 3]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                        alreadyAdded.Add($"{i + 1},{j + 2}");
                                        alreadyAdded.Add($"{i + 1},{j + 3}");
                                    }
                                }
                                else if (amountOnRight == 1)
                                {
                                    string toFind = $"{i + 1},{j + 1};{i + 1},{j + 2}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j + 1].ToString() + charGrid[i + 1, j + 2]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                        alreadyAdded.Add($"{i + 1},{j + 2}");
                                    }
                                }
                                else
                                {
                                    string toFind = $"{i + 1},{j + 1}";
                                    if (!AlreadyOnList(toFind))
                                    {
                                        result += int.Parse((charGrid[i + 1, j + 1]).ToString());
                                        alreadyAdded.Add($"{i + 1},{j + 1}");
                                    }
                                }
                            }
                        }

                    }
                }
            }
            Console.WriteLine("Sum of part numbers: " + result);
        }

        public static void SecondPart(string documentPath)
        {
            return;
        }

        public static char[,] ToGrid(string documentPath)
        {
            string[] lines = File.ReadAllLines(documentPath);
            int i = 0, j = 0;
            char[,] result = new char[lines[0].Length, lines.Length];
            foreach (var row in lines)
            {
                j = 0;
                foreach (var col in row)
                {
                    result[i, j] = col;
                    j++;
                }
                i++;
            }
            return result;
        }

        public static int DigitsOnSide(char[,] grid, int currentX, int currentY, bool searchLeft = true)
        {
            if (searchLeft)
            {
                if (char.IsDigit(grid[currentY, currentX - 1]))
                    if (char.IsDigit(grid[currentY, currentX - 2]))
                        return 2;
                    else
                        return 1;
                else
                    return 0;
            }
            else
            {
                if (char.IsDigit(grid[currentY, currentX + 1]))
                    if (char.IsDigit(grid[currentY, currentX + 2]))
                        return 2;
                    else
                        return 1;
                else
                    return 0;
            }
        }

        public static bool AlreadyOnList(string positionToSearch)
        {
            List<string> toSearch = [.. positionToSearch.Split(";")];
            return alreadyAdded.Intersect(toSearch).Any();
        }
    }
}
