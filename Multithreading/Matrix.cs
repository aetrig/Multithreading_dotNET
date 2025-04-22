using System.Runtime.CompilerServices;

namespace Multithreading;

internal class Matrix
{
	public int rowsCount { get; }
	public int colsCount { get; }
	public int[,] values { get; }

	//Non-positive or absent cols value creates a square matrix
	//Negative seed value creates 0 matrix
	public Matrix(int rows, int cols = -1, int seed = 0)
	{
		rowsCount = rows;
		colsCount = cols > 0 ? cols : rows;
		values = new int[rowsCount, colsCount];
		if (seed >= 0)
		{
			Random rand = new(seed);
			for (int row = 0; row < rowsCount; row++)
			{
				for (int col = 0; col < colsCount; col++)
				{
					values[row, col] = rand.Next() % 10;
				}
			}
		}
		if (seed < 0)
		{
			for (int row = 0; row < rowsCount; row++)
			{
				for (int col = 0; col < colsCount; col++)
				{
					values[row, col] = 0;
				}
			}
		}
	}

	public override string ToString()
	{
		string output = "";
		for (int row = 0; row < rowsCount; row++)
		{
			for (int col = 0; col < colsCount; col++)
			{
				output += $"{values[row, col]} ";
			}
			output += "\n";
		}
		return output;
	}
}
