namespace AdventOfCode2022
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Day1();
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
		}
	}
}