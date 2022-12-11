namespace AdventOfCode2022
{
	internal class d11
	{
		public class Moneky
		{
			public enum MonkeyOp
			{
				Mul,
				Add
			}

			public Queue<long> StartingItems { get; set; } = new Queue<long>();
			public MonkeyOp Operation { get; set; }
			public long OperationValue { get; set; }
			public long DivisibleCheck { get; set; }
			public int TrueIndex { get; set; }
			public int FalseIndex { get; set; }
		}

		protected enum Part
		{
			P1,
			P2
		}

		public long Part1Answer { get; set; } = 0;
		public long Part2Answer { get; set; } = 0;
		public string Input { get; set; }

		private List<Moneky> Monkies = new List<Moneky>();

		public void Solve()
		{
			ParseInput();
			Process(Part.P1);
			ParseInput();
			Process(Part.P2);

			Console.WriteLine($"Day 11 Part 1 Solution: {Part1Answer}");
			Console.WriteLine($"Day 11 Part 2 Solution: {Part2Answer}");			
			Console.WriteLine();
		}

		protected void ParseInput()
		{
			Monkies = new List<Moneky>();
			var splitInput = Input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
			foreach (var item in splitInput)
			{
				var splitMonkey = item.Split(Environment.NewLine);
				var newMonkey = new Moneky();

				newMonkey.StartingItems = new Queue<long>(splitMonkey[1].Split(':')[1].Split(',').Select(x => long.Parse(x)));

				if (splitMonkey[2].Contains('*'))
					newMonkey.Operation = Moneky.MonkeyOp.Mul;
				else
					newMonkey.Operation = Moneky.MonkeyOp.Add;

				var opValue = splitMonkey[2].Split(' ').Last();
				if (opValue == "old")
					newMonkey.OperationValue = 0;
				else
					newMonkey.OperationValue = long.Parse(opValue);

				newMonkey.DivisibleCheck = long.Parse(splitMonkey[3].Split(' ').Last());
				newMonkey.TrueIndex = int.Parse(splitMonkey[4].Split(' ').Last());
				newMonkey.FalseIndex = int.Parse(splitMonkey[5].Split(' ').Last());

				Monkies.Add(newMonkey);
			}
		}

		protected void Process(Part part)
		{
			var inspected = new long[Monkies.Count()];

			//Part 2 - Find LCM for modulo to keep items from growing too large
			long leastCommonMult = 1;
			foreach (var item in Monkies)
			{
				leastCommonMult *= item.DivisibleCheck;
			}

			int runLength = 20;
			if (part == Part.P2)
				runLength = 10000;

			for (int a = 0; a < runLength; a++)
			{
				for (int i = 0; i < Monkies.Count(); i++)
				{
					while (Monkies[i].StartingItems.Count > 0)
					{
						inspected[i]++;
						var item = Monkies[i].StartingItems.Dequeue();

						if (part == Part.P2)
							item %= leastCommonMult;

						if (Monkies[i].Operation == Moneky.MonkeyOp.Add)
							item += Monkies[i].OperationValue;
						else
							if (Monkies[i].OperationValue == 0)
								item *= item;
							else
								item *= Monkies[i].OperationValue;

						if (part == Part.P1)
							item /= 3;

						if (item % Monkies[i].DivisibleCheck == 0)
							Monkies[Monkies[i].TrueIndex].StartingItems.Enqueue(item);
						else
							Monkies[Monkies[i].FalseIndex].StartingItems.Enqueue(item);
					}
				}
			}

			if (part == Part.P1)
				Part1Answer = inspected.OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => x * y);
			else
				Part2Answer = inspected.OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => x * y);
		}
	}
}