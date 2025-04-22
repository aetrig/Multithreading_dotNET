using System;

namespace Multithreading;

internal class MatrixMultiplier
{
	public Matrix m1 { get;set; }
	public Matrix m2 { get; set; }
	private volatile Matrix _result;
	public Matrix result { 
		get {return _result; } 
		}

	public MatrixMultiplier(Matrix m1, Matrix m2)
	{
		this.m1 = m1;
		this.m2 = m2;
		_result = new(0);
	}

	public void MultiplyParallel(int maxThreads)
	{
		_result = new(m1.rowsCount, m2.colsCount, -1);
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
				_result.values[row, col] = value;
			}
		});
	}

	public void MultiplyThreads(int threadsCount)
	{
		_result = new(m1.rowsCount, m2.colsCount, -1);
		
		//Matrixes aren't multipliable
		if (m1.colsCount != m2.rowsCount)
		{
			Console.WriteLine("Not multipliable");
			return;
		}
		
		Thread[] threads = new Thread[threadsCount];
		//int row = 0;
		for (int row = 0; row < m1.rowsCount; row += threadsCount)
		{
			int i = 0;
			while (i < threadsCount && row < m1.rowsCount)
			{
				threads[i] = new Thread(new ParameterizedThreadStart(calculateRow));
				threads[i].Start(row);
				row++;
				i++;
			}
			for (int n = 0; n < i; n++)
			{
				threads[n].Join();
			}
		}
	}

	private void calculateRow(object? _row)
	{
		int row = (int) (_row ?? 0);
		for (int col = 0; col < m2.colsCount; col++)
		{
			int value = 0;
			for (int i = 0; i < m1.colsCount; i++)
			{
				value += m1.values[row, i] * m2.values[i, col];
			}
			_result.values[row, col] = value;
		}
	}
}
