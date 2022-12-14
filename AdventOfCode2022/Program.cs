using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AdventOfCode2022
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Day1();
			//Day2();
			//Day3();
			//Day4();
			//Day5();
			//Day6();
			//Day7();
			//Day8();
			//Day9();
			//Day10();
			//Day11();
			//Day12();
			//Day13();
			Day14();
		}

		public static void Day1()
		{
			var day1Input = File.ReadAllLines(@"Day01.txt");
			var values = new List<int>();

			int currentValue = 0;

			foreach (var item in day1Input)
			{
				if (string.IsNullOrEmpty(item))
				{
					values.Add(currentValue);
					currentValue = 0;
				}
				else
				{
					currentValue += int.Parse(item);
				}
			}

			Console.WriteLine($"Day 1 Part 1 Solution: {values.Max()}");
			Console.WriteLine($"Day 1 Part 2 Solution: {values.OrderByDescending(x => x).Take(3).Sum()}");
			Console.WriteLine();
		}

		public static void Day2()
		{
			var input = File.ReadAllLines(@"Day02.txt").Select(x => x.Split(' '));

			int part1Answer = 0;
			int part2Answer = 0;

			foreach (var item in input)
			{
				switch (item[1])
				{
					case "X":
						part1Answer += 1;
						switch (item[0])
						{
							case "A":
								part1Answer += 3;
								part2Answer += 3;
								break;
							case "B":
								part2Answer += 1;
								break;
							case "C":
								part1Answer += 6;
								part2Answer += 2;
								break;
						}
						break;
					case "Y":
						part1Answer += 2;
						part2Answer += 3;
						switch (item[0])
						{
							case "A":
								part1Answer += 6;
								part2Answer += 1;
								break;
							case "B":
								part1Answer += 3;
								part2Answer += 2;
								break;
							case "C":
								part2Answer += 3;
								break;
						}
						break;
					case "Z":
						part1Answer += 3;
						part2Answer += 6;
						switch (item[0])
						{
							case "A":
								part2Answer += 2;
								break;
							case "B":
								part1Answer += 6;
								part2Answer += 3;
								break;
							case "C":
								part1Answer += 3;
								part2Answer += 1;
								break;
						}
						break;
				}
			}

			Console.WriteLine($"Day 2 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 2 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day3()
		{
			var input = File.ReadAllLines(@"Day03.txt");

			int part1Answer = 0;
			int part2Answer = 0;

			foreach (var item in input)
			{
				var sackA = item.Substring(0, item.Length / 2);
				var sackB = item.Substring(item.Length / 2);

				var overlap = sackA.Intersect(sackB).First();

				if (char.IsUpper(overlap))
					part1Answer += overlap - 38;
				else
					part1Answer += overlap - 96;
			}

			for (int i = 0; i < input.Length; i += 3)
			{
				var overlap = input[i].Intersect(input[i+1]).Intersect(input[i+2]).First();

				if (char.IsUpper(overlap))
					part2Answer += overlap - 38;
				else
					part2Answer += overlap - 96;
			}

			Console.WriteLine($"Day 3 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 3 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day4()
		{
			var input = File.ReadAllLines(@"Day04.txt");

			int part1Answer = 0;
			int part2Answer = 0;

			foreach (var item in input)
			{
				var split = item.Split(',').Select(x => x.Split('-').Select(y => int.Parse(y)).ToArray()).ToArray();
				var e1l = split[0][0];
				var e1h = split[0][1];
				var e2l = split[1][0];
				var e2h = split[1][1];

				if (e2l >= e1l && e2h <= e1h)
				{
					part1Answer++;
				}
				else if (e1l >= e2l && e1h <= e2h)
				{
					part1Answer++;
				}
				else if ((e2l >= e1l && e2l <= e1h) || (e2h >= e1l && e2h <= e1h))
				{
					part2Answer++;
				}
				else if ((e1l >= e2l && e1l <= e2h) || (e1h >= e2l && e1h <= e2h))
				{
					part2Answer++;
				}
			}

			part2Answer += part1Answer;

			Console.WriteLine($"Day 4 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 4 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day5()
		{
			var input = File.ReadAllLines(@"Day05.txt");
			var size = (input[0].Length + 1) / 4;

			string part1Answer = String.Empty;
			string part2Answer = String.Empty;

			var p1Stacks = new List<Stack<char>>();
			var p2Stacks = new List<Stack<char>>();
			var inputStacks = new List<string>();

			for (int i = 0; i < size; i++)
			{
				p1Stacks.Add(new Stack<char>());
				p2Stacks.Add(new Stack<char>());
				inputStacks.Add(string.Empty);
			}

			foreach (var item in input)
			{
				if (item.Contains('[') || item.Contains(']'))
				{
					for (int i = 1; i < item.Length; i += 4)
					{
						if (item[i] != ' ')
						{
							inputStacks[i / 4] += item[i];
						}
					}
				}
				else if (string.IsNullOrEmpty(item))
				{
					for (int i = 0; i < size; i++)
					{
						var reversedInput = inputStacks[i].Reverse();
						foreach (var c in reversedInput)
						{
							p1Stacks[i].Push(c);
							p2Stacks[i].Push(c);
						}
					}
				}
				else if (item.Contains("move"))
				{
					var splits = item.Split(' ');
					var count = int.Parse(splits[1]);
					var from = int.Parse(splits[3]) - 1;
					var to = int.Parse(splits[5]) - 1;

					var p2Temp = string.Empty;

					while (count > 0)
					{
						count--;

						//part 1
						p1Stacks[to].Push(p1Stacks[from].Pop());

						//part 2
						p2Temp += p2Stacks[from].Pop();
					}

					//more part 2
					for (int j = p2Temp.Length; j > 0; j--)
					{
						p2Stacks[to].Push(p2Temp[(j - 1)]);
					}
				}
			}

			foreach (var item in p1Stacks)
			{
				part1Answer += item.Pop();
			}

			foreach (var item in p2Stacks)
			{
				part2Answer += item.Pop();
			}

			Console.WriteLine($"Day 5 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 5 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day6()
		{
			var input = File.ReadAllLines(@"Day06.txt").First();

			int p1Width = 4;
			int p2Width = 14;

			var part1Answer = 0;
			var part2Answer = 0;

			for (int i = 0; i < input.Length; i++)
			{
				var temp = input.Substring(i, p1Width);
				if (temp.GroupBy(x => x).Count() == p1Width)
				{
					part1Answer = i + p1Width;
					break;
				}
			}

			for (int i = 0; i < input.Length; i++)
			{
				var temp = input.Substring(i, p2Width);
				if (temp.GroupBy(x => x).Count() == p2Width)
				{
					part2Answer = i + p2Width;
					break;
				}
			}

			Console.WriteLine($"Day 6 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 6 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day7()
		{
			var daySeven = new d7(File.ReadAllLines(@"Day07.txt"));
			daySeven.Solve();
		}

		public static void Day8()
		{
			var input = File.ReadAllLines(@"Day08.txt");

			var part1Answer = 0;
			var part2Answer = 0;

			int width = input[0].Length;
			int height = input.Length;

			var grid = new int[height, width];

			for (int i = 0; i < height; i++)
				for (int j = 0; j < width; j++)
					grid[i, j] = input[i][j] - 48;

			//Edge Trees
			part1Answer += (2 * height) + ((2 * width) - 4);

			for (int i = 1; i < height - 1; i++)
			{
				for (int j = 1; j < width - 1; j++)
				{
					//Check Left
					bool visible = true;
					int tempIndex = j - 1;
					while (tempIndex >= 0 && visible)
					{
						if (grid[i, j] > grid[i, tempIndex])
							tempIndex--;
						else
							visible = false;
					}
					if (visible)
					{
						part1Answer++;
						continue;
					}

					//Check Right
					visible = true;
					tempIndex = j + 1;
					while (tempIndex < width && visible)
					{
						if (grid[i, j] > grid[i, tempIndex])
							tempIndex++;
						else
							visible = false;
					}
					if (visible)
					{
						part1Answer++;
						continue;
					}

					//Check Up
					visible = true;
					tempIndex = i - 1;
					while (tempIndex >= 0 && visible)
					{
						if (grid[i, j] > grid[tempIndex, j])
							tempIndex--;
						else
							visible = false;
					}
					if (visible)
					{
						part1Answer++;
						continue;
					}

					//Check Down
					visible = true;
					tempIndex = i + 1;
					while (tempIndex < height && visible)
					{
						if (grid[i, j] > grid[tempIndex, j])
							tempIndex++;
						else
							visible = false;
					}
					if (visible)
					{
						part1Answer++;
						continue;
					}
				}
			}

			for (int i = 1; i < height - 1; i++)
			{
				for (int j = 1; j < width - 1; j++)
				{
					int leftScenic = 1;
					int tempIndex = j - 1;
					bool tallerSeen = false;
					while (tempIndex >= 0 && !tallerSeen)
					{
						if (grid[i, j] > grid[i, tempIndex])
						{
							tempIndex--;
							leftScenic++;
						}
						else
							tallerSeen = true;
					}
					//Don't double count since we reached the edge
					if (!tallerSeen)
						leftScenic--;

					int rightScenic = 1;
					tempIndex = j + 1;
					tallerSeen = false;
					while (tempIndex < width && !tallerSeen)
					{
						if (grid[i, j] > grid[i, tempIndex])
						{
							tempIndex++;
							rightScenic++;
						}
						else
							tallerSeen = true;
					}
					if (!tallerSeen)
						rightScenic--;

					int upScenic = 1;
					tempIndex = i - 1;
					tallerSeen = false;
					while (tempIndex >= 0 && !tallerSeen)
					{
						if (grid[i, j] > grid[tempIndex, j])
						{
							tempIndex--;
							upScenic++;
						}
						else
							tallerSeen = true;
					}
					if (!tallerSeen)
						upScenic--;

					int downScenic = 1;
					tempIndex = i + 1;
					tallerSeen = false;
					while (tempIndex < height && !tallerSeen)
					{
						if (grid[i, j] > grid[tempIndex, j])
						{
							tempIndex++;
							downScenic++;
						}
						else
							tallerSeen = true;
					}
					if (!tallerSeen)
						downScenic--;

					int scenicScore = leftScenic * rightScenic * upScenic * downScenic;

					if (scenicScore > part2Answer)
					{
						part2Answer = scenicScore;
					}
				}
			}

			Console.WriteLine($"Day 8 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 8 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static void Day9()
		{
			var input = File.ReadAllLines(@"Day09.txt");

			int knotCount = 10;
			var knots = new Point[knotCount];
			for (int i = 0; i < 10; i++)
				knots[i] = new Point(0, 0);

			var part1Knot = 1;
			var part2Knot = 9;

			var part1Points = new List<Point>() { knots[part1Knot] };
			var part2Points = new List<Point>() { knots[part2Knot] };

			foreach (var item in input)
			{
				var splitInput = item.Split(' ');
				int dist = int.Parse(splitInput[1]);
				for (int i = 0; i < dist; i++)
				{
					if (splitInput[0] == "R")
						knots[0].X++;
					else if (splitInput[0] == "L")
						knots[0].X--;
					else if (splitInput[0] == "U")
						knots[0].Y++;
					else if (splitInput[0] == "D")
						knots[0].Y--;

					//Move Knots
					for (int j = 1; j < knots.Length; j++)
					{
						int xDiff = knots[j - 1].X - knots[j].X;
						int yDiff = knots[j - 1].Y - knots[j].Y;

						if (xDiff > 1 || xDiff < -1)
						{
							knots[j].X += 1 * Math.Sign(xDiff);

							if (yDiff > 0)
								knots[j].Y++;
							else if (yDiff < 0)
								knots[j].Y--;
						}
						else if (yDiff > 1 || yDiff < -1)
						{
							knots[j].Y += 1 * Math.Sign(yDiff);

							if (xDiff > 0)
								knots[j].X++;
							else if (xDiff < 0)
								knots[j].X--;
						}
					}

					part1Points.Add(knots[part1Knot]);
					part2Points.Add(knots[part2Knot]);
				}
			}

			Console.WriteLine($"Day 9 Part 1 Solution: {part1Points.GroupBy(g => new { g.X, g.Y }).Select(x => x.First()).Count()}");
			Console.WriteLine($"Day 9 Part 2 Solution: {part2Points.GroupBy(g => new { g.X, g.Y }).Select(x => x.First()).Count()}");
			Console.WriteLine();
		}

		public static void Day10()
		{
			var input = File.ReadAllLines(@"Day10.txt");

			var Day10 = new d10() { Input = input };
			Day10.Solve();
		}

		public static void Day11()
		{
			var input = File.ReadAllText(@"Day11.txt");

			var Day11 = new d11() { Input = input };
			Day11.Solve();
		}

		public static void Day12()
		{
			var input = File.ReadAllLines(@"Day12.txt");

			var Day12 = new d12() { Input = input };
			Day12.Solve();
		}

		public static void Day13()
		{
			var input = File.ReadAllLines(@"Day13.txt").Where(x => !string.IsNullOrEmpty(x)).ToArray();

			var Day13 = new d13() { Input = input };
			Day13.Solve();
		}

		public static void Day14()
		{
			var input = File.ReadAllLines(@"Day14.txt");

			var part1Answer = 0;
			var part2Answer = 0;

			int size = 1000;

			var sandStart = new Point(500, 0);
			int[,] grid = new int[size, size];
			var lowestPoint = 0;

			foreach (var item in input)
			{
				var splitItems = item.Split(" -> ").Select(x => x.Split(',')).Select(x => new Point(int.Parse(x[0]), int.Parse(x[1]))).ToArray();

				for (int i = 0; i < splitItems.Length - 1; i++)
				{
					var a = splitItems[i];
					var b = splitItems[i + 1];

					if (a.X == b.X)
					{
						int maxY = Math.Max(a.Y, b.Y);
						int minY = Math.Min(a.Y, b.Y);
						for (int j = minY; j <= maxY; j++)
							grid[a.X, j] = -1;

						if (maxY > lowestPoint)
							lowestPoint = maxY;
					}
					else
					{
						int maxX = Math.Max(a.X, b.X);
						int minX = Math.Min(a.X, b.X);
						for (int j = minX; j <= maxX; j++)
							grid[j, a.Y] = -1;

						if (a.Y > lowestPoint)
							lowestPoint = a.Y;
					}
				}
			}

			for (int i = 0; i < size; i++)
			{
				grid[i, lowestPoint + 2] = -1;
			}

			int count = 0;
			while (true)
			{
				if (grid[sandStart.X, sandStart.Y] == 1)
				{
					part2Answer = count;
					break;
				}
				else
				{
					var currentSandPoint = new Point(sandStart.X, sandStart.Y);

					while (true)
					{
						currentSandPoint.Y++;

						if (currentSandPoint.Y > lowestPoint && part1Answer == 0)
						{
							part1Answer = count;
							break;
						}

						if (grid[currentSandPoint.X, currentSandPoint.Y] == 0)
							continue;

						currentSandPoint.X--;
						if (grid[currentSandPoint.X, currentSandPoint.Y] == 0)
							continue;

						currentSandPoint.X += 2;
						if (grid[currentSandPoint.X, currentSandPoint.Y] == 0)
							continue;

						//Settled, reset
						currentSandPoint.X--;
						currentSandPoint.Y--;
						count++;
						grid[currentSandPoint.X, currentSandPoint.Y] = 1;
						break;
					}
				}
			}

			Console.WriteLine($"Day 14 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 14 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}
	}
}