namespace AdventOfCode2022
{
	internal class d7
	{
		public string[] input { get; set; } = null;

		public d7(string[] Input)
		{
			input = Input;
		}

		public void Solve()
		{
			var part1Answer = 0;
			var part2Answer = 0;

			var root = new d7Dir() { Dirname = "root" };
			var currentDirectory = root;

			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == "$ ls")
				{
					while (true)
					{
						i++;
						if (i == input.Length || input[i].StartsWith("$"))
						{
							i -= 1;
							break;
						}

						var splitInput = input[i].Split(' ');

						if (splitInput[0] == "dir")
						{
							if (currentDirectory.Subfolders.Any(x => x.Dirname.Equals(splitInput[1])))
								continue;

							currentDirectory.Subfolders.Add(new d7Dir() { Dirname = splitInput[1], Parent = currentDirectory });
						}
						else
						{
							if (currentDirectory.Files.Any(x => x.Filename.Equals(splitInput[1])))
								continue;

							currentDirectory.Files.Add(new d7File() { Filesize = int.Parse(splitInput[0]), Filename = splitInput[1] });
						}
					}
				}
				else if (input[i] == "$ cd /")
				{
					currentDirectory = root;
				}
				else if (input[i] == "$ cd ..")
				{
					currentDirectory = currentDirectory.Parent;
				}
				else if (input[i].Contains("$ cd "))
				{
					var dirToUse = input[i].Split(' ').Last();
					for (int j = 0; j < currentDirectory.Subfolders.Count; j++)
					{
						if (currentDirectory.Subfolders[j].Dirname == dirToUse)
						{
							currentDirectory = currentDirectory.Subfolders[j];
							break;
						}
					}
				}
			}

			//Part 1
			part1Answer = root.Part1FolderSize();

			//Part 2
			currentUsed = totalDiskSpace - root.Foldersize();
			root.Part2FreeUpSpace();
			part2Answer = drivesToDelete.Min();

			Console.WriteLine($"Day 7 Part 1 Solution: {part1Answer}");
			Console.WriteLine($"Day 7 Part 2 Solution: {part2Answer}");
			Console.WriteLine();
		}

		public static List<int> drivesToDelete = new();

		public static int totalDiskSpace = 70000000;
		public static int unusedTarget = 30000000;
		public static int currentUsed;

		public class d7File
		{
			public string Filename { get; set; } = string.Empty;
			public int Filesize { get; set; } = 0;
		}

		public class d7Dir
		{
			public string Dirname { get; set; } = string.Empty;
			public d7Dir? Parent { get; set; } = null;
			public List<d7Dir> Subfolders = new();
			public List<d7File> Files = new();

			public int Foldersize()
			{
				var size = 0;

				foreach (var item in Files)
					size += item.Filesize;

				foreach (var item in Subfolders)
					size += item.Foldersize();

				return size;
			}

			public int Part1FolderSize()
			{
				var result = 0;

				var foldersize = Foldersize();

				if (foldersize < 100000)
					result += foldersize;

				foreach (var item in Subfolders)
					result += item.Part1FolderSize();

				return result;
			}

			public void Part2FreeUpSpace()
			{
				var currentFoldersize = Foldersize();

				if ((currentFoldersize + currentUsed) > unusedTarget)
					drivesToDelete.Add(currentFoldersize);

				foreach (var item in Subfolders)
					item.Part2FreeUpSpace();
			}
		}
	}
}