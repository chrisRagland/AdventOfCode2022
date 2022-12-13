using System.Text.Json;

namespace AdventOfCode2022
{
	internal class d13
	{
		protected class Packet { }
		protected class PacketValue : Packet
		{
			public int Value { get; set; }
			public PacketValue(int value)
			{
				Value = value;
			}
		}
		protected class PacketList : Packet
		{
			public List<Packet> List { get; set; }
			public PacketList(List<Packet> list)
			{
				List = list;
			}
		}

		protected Packet ParseString(string input)
		{
			var rawJson = JsonSerializer.Deserialize<JsonElement>(input);
			return ParseJSON(rawJson);
		}

		protected Packet ParseJSON(JsonElement element)
		{
			if (element.ValueKind == JsonValueKind.Number)
				return new PacketValue(element.GetInt32());
			else
				return new PacketList(new List<Packet>(element.EnumerateArray().Select(x => ParseJSON(x))));
		}

		protected int Compare(Packet left, Packet right)
		{
			if (left == null && right != null)
				return -1;
			else if (left != null && right == null)
				return 1;
			else if (left is PacketValue && right is PacketValue)
				return Comparer<int>.Default.Compare(((PacketValue)left).Value, ((PacketValue)right).Value);
			else if (left is PacketValue && right is PacketList)
				return Compare(new PacketList(new List<Packet>() { left }), right);
			else if (left is PacketList && right is PacketValue)
				return Compare(left, new PacketList(new List<Packet>() { right }));
			else
			{
				var returnValue = 0;
				var leftList = ((PacketList)left).List;
				var rightList = ((PacketList)right).List;
				int index = 0;
				while (returnValue == 0)
				{
					var leftItem = leftList.ElementAtOrDefault(index);
					var rightItem = rightList.ElementAtOrDefault(index);
					if (leftItem == null && rightItem == null)
						return 0;

					returnValue = Compare(leftItem, rightItem);
					index++;
				}
				return returnValue;
			}
		}

		public string[] Input { get; set; }
		public int Part1Answer { get; set; } = 0;
		public int Part2Answer { get; set; } = 0;

		public void Solve()
		{
			List<Packet> packets = new List<Packet>();
			foreach (var item in Input)
				packets.Add(ParseString(item));

			int index = 1;
			for (int i = 0; i < packets.Count(); i += 2)
			{
				if (Compare(packets[i], packets[i+1]) < 0)
					Part1Answer += index;

				index++;
			}

			var twoPacket = ParseString("[[2]]");
			packets.Add(twoPacket);
			var sixPacket = ParseString("[[6]]");
			packets.Add(sixPacket);

			var sortedPackets = packets.OrderBy(x => x, Comparer<Packet>.Create(Compare)).ToList();

			var twoIndex = sortedPackets.IndexOf(twoPacket) + 1;
			var sixIndex = sortedPackets.IndexOf(sixPacket) + 1;

			Part2Answer = twoIndex * sixIndex;

			Console.WriteLine($"Day 13 Part 1 Solution: {Part1Answer}");
			Console.WriteLine($"Day 13 Part 2 Solution: {Part2Answer}");
			Console.WriteLine();
		}
	}
}