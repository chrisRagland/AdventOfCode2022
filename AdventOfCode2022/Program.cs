namespace AdventOfCode2022
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Day1();
			Day2();
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

			Console.WriteLine($"Day 1 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 1 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}
	}
}