using System;

namespace Multithreading;

internal class MatrixMultiplier
{
	public Matrix m1 { get;set; }
	public Matrix m2 { get; set; }
	public Matrix result { get; private set; }

	public MatrixMultiplier(Matrix m1, Matrix m2)
	{
		this.m1 = m1;
		this.m2 = m2;
		result = new(0);
	}

	public void MultiplyParallel(int maxThreads)
	{
		result = new(m1.rowsCount, m2.colsCount, -1);
		//Matrixes aren't multipliable
		if (m1.colsCount != m2.rowsCount)
		{
			Console.WriteLine("Not multipliable");
			return;
		}
		ParallelOptions options = new() { MaxDegreeOfParallelism = maxThreads };
		Parallel.For(0, m1.rowsCount, options, row =>
		{
			for (int col = 0; col < m2.colsCount; col++)
			{
				int value = 0;
				for (int i = 0; i < m1.colsCount; i++)
				{
					value += m1.values[row, i] * m2.values[i, col];
				}
				result.values[row, col] = value;
			}
		});
	}

	public void MultiplyThreads(int maxThreads)
	{
		Matrix result = new(m1.rowsCount, m2.colsCount, -1);
		//Matrixes aren't multipliable
		if (m1.colsCount != m2.rowsCount)
		{
			Console.WriteLine("Not multipliable");
			return;
		}
	}
}
