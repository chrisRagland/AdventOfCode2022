namespace AdventOfCode2022
{
	internal class d10
	{
		public int Part1Answer { get; set; } = 0;
		public System.Text.StringBuilder Part2Answer { get; set; } = new System.Text.StringBuilder();
		public string[] Input { get; set; }
		private int x = 1;
		private int cycle = 0;

		public void Solve()
		{
			foreach (var item in Input)
			{
				ProcessCycle();
				if (item != "noop")
				{
					ProcessCycle();
					var addXValue = int.Parse(item.Split(' ').Last());
					x += addXValue;
				}
			}

			Console.WriteLine($"Day 10 Part 1 Solution: {Part1Answer}");
			Console.WriteLine($"Day 10 Part 2 Solution:");
			Console.Write(Part2Answer.ToString());
			Console.WriteLine();

		}

		private void ProcessCycle()
		{
			cycle++;

			//Part 1
			if (cycle % 40 == 20)
				Part1Answer += x * cycle;

			//Part 2
			if (Math.Abs(((cycle - 1) % 40) - x) < 2)
				Part2Answer.Append('#');
			else
				Part2Answer.Append(' ');

			if (cycle % 40 == 0)
				Part2Answer.AppendLine();
		}
	}
}