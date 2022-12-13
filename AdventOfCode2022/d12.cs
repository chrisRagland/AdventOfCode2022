using System.Drawing;

namespace AdventOfCode2022
{
	internal class d12
	{
		public class MazeStates
		{
			public Point Current { get; set; } = new Point();
			public int StepCount { get; set; } = 0;
		}

		public string[] Input { get; set; }
		public long Part1Answer { get; set; } = 0;
		public long Part2Answer { get; set; } = 0;

		protected Point endPoint;
		protected int[,] grid;
		protected int[,] fastestVisited;
		protected Queue<MazeStates> states;
		protected int CurrentSolution = int.MaxValue;

		protected enum Part
		{
			P1,
			P2
		}

		public void Solve()
		{
			Process(Part.P1);
			Process(Part.P2);

			Console.WriteLine($"Day 12 Part 1 Solution: {Part1Answer}");
			Console.WriteLine($"Day 12 Part 2 Solution: {Part2Answer}");
			Console.WriteLine();
		}

		protected void Process(Part part)
		{
			var startPoint = new List<Point>();
			int maxX = Input.Length;
			int maxY = Input[0].Length;

			grid = new int[maxX, maxY];
			fastestVisited = new int[maxX, maxY];
			states = new Queue<MazeStates>();

			for (int i = 0; i < maxX; i++)
			{
				for (int j = 0; j < maxY; j++)
				{
					fastestVisited[i, j] = int.MaxValue;
					if (Input[i][j] == 'S' || (part == Part.P2 && j == 0))
					{
						var tempStartPoint = new Point(i, j);
						startPoint.Add(tempStartPoint);
						grid[i, j] = 1;
					}
					else if (Input[i][j] == 'E')
					{
						endPoint = new Point(i, j);
						grid[i, j] = 26;
					}
					else
					{
						grid[i, j] = Input[i][j] - 96;
					}
				}
			}

			foreach (var item in startPoint)
				states.Enqueue(new MazeStates() { Current = item });

			while (states.Count > 0)
			{
				var CurrentState = states.Dequeue();

				//Discard if this can't ever be the solution
				if (CurrentState.StepCount > CurrentSolution)
					continue;

				//Up
				if (CurrentState.Current.X > 0 && grid[CurrentState.Current.X, CurrentState.Current.Y] + 1 >= grid[CurrentState.Current.X - 1, CurrentState.Current.Y])
					ProcessStep(new Point(CurrentState.Current.X - 1, CurrentState.Current.Y), CurrentState.StepCount + 1);

				//Down
				if (CurrentState.Current.X < (maxX - 1) && grid[CurrentState.Current.X, CurrentState.Current.Y] + 1 >= grid[CurrentState.Current.X + 1, CurrentState.Current.Y])
					ProcessStep(new Point(CurrentState.Current.X + 1, CurrentState.Current.Y), CurrentState.StepCount + 1);

				//Left
				if (CurrentState.Current.Y > 0 && grid[CurrentState.Current.X, CurrentState.Current.Y] + 1 >= grid[CurrentState.Current.X, CurrentState.Current.Y - 1])
					ProcessStep(new Point(CurrentState.Current.X, CurrentState.Current.Y - 1), CurrentState.StepCount + 1);

				//Right
				if (CurrentState.Current.Y < (maxY - 1) && grid[CurrentState.Current.X, CurrentState.Current.Y] + 1 >= grid[CurrentState.Current.X, CurrentState.Current.Y + 1])
					ProcessStep(new Point(CurrentState.Current.X, CurrentState.Current.Y + 1), CurrentState.StepCount + 1);
			}

			if (part == Part.P1)
				Part1Answer = CurrentSolution;
			else if (part == Part.P2)
				Part2Answer = CurrentSolution;
		}

		private void ProcessStep(Point newPoint, int newStepCount)
		{
			if (fastestVisited[newPoint.X, newPoint.Y] > newStepCount)
				if (newPoint.X == endPoint.X && newPoint.Y == endPoint.Y)
					if (newStepCount < CurrentSolution)
						CurrentSolution = newStepCount;
				else
				{
					states.Enqueue(new MazeStates() { Current = newPoint, StepCount = newStepCount });
					fastestVisited[newPoint.X, newPoint.Y] = newStepCount;
				}
		}
	}
}