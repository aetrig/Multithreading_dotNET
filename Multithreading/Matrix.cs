using System.Runtime.CompilerServices;

namespace Multithreading;

public class Matrix
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

	public Matrix MultiplyNonThreaded(Matrix m2)
	{
		Matrix result = new(rowsCount, m2.colsCount, -1);
		//Matrixes aren't multipliable
		if (colsCount != m2.rowsCount)
		{
			return new(0);
		}

		for (int row = 0; row < rowsCount; row++)
		{
			for (int col = 0; col < m2.colsCount; col++)
			{
				int value = 0;
				for (int i = 0; i < colsCount; i++)
				{
					value += values[row, i] * m2.values[i, col];
				}
				result.values[row, col] = value;
			}
		}
		return result;
	}
	public Matrix MultiplyParallel(Matrix m2, int maxThreads)
	{
		Matrix result = new(rowsCount, m2.colsCount, -1);
		//Matrixes aren't multipliable
		if (colsCount != m2.rowsCount)
		{
			return new(0);
		}
		ParallelOptions options = new() { MaxDegreeOfParallelism = maxThreads};
		Parallel.For(0, rowsCount, options, row =>
		{
			for (int col = 0; col < m2.colsCount; col++)
			{
				int value = 0;
				for (int i = 0; i < colsCount; i++)
				{
					value += values[row, i] * m2.values[i, col];
				}
				result.values[row, col] = value;
			}
		});

		return result;
	}
}
