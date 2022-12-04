namespace AdventOfCode2022
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Day1();
			//Day2();
			//Day3();
			Day4();
		}

		public static void Day1()
		{
			var day1Input = File.ReadAllLines(@"Day1.txt");
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
			var input = File.ReadAllLines(@"Day2.txt").Select(x => x.Split(' '));

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
			var input = File.ReadAllLines(@"Day3.txt");

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
			var input = File.ReadAllLines(@"Day4.txt");

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
	}
}