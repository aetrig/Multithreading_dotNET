using System;

namespace Multithreading;

internal class TwoInts
{
	public int num1 { get; set; }
	public int num2 { get; set; }

	public TwoInts(int n1, int n2)
	{
		num1 = n1;
		num2 = n2;
	}
}


internal class MatrixMultiplier
{
	public Matrix m1 { get; set; }
	public Matrix m2 { get; set; }
	private volatile Matrix _result;
	public Matrix result
	{
		get { return _result; }
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

	public void MultiplyThreadsOld(int threadsCount)
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

	public void MultiplyThreads(int threadCount)
	{
		_result = new(m1.rowsCount, m2.colsCount, -1);

		//Matrixes aren't multipliable
		if (m1.colsCount != m2.rowsCount)
		{
			Console.WriteLine("Not multipliable");
			return;
		}
		Thread[] threads = new Thread[threadCount];
		int rowPerThread = (int) Math.Ceiling(m1.rowsCount/(double) threadCount);
		for(int i = 0; i < threadCount; i++)
		{
			threads[i] = new Thread(new ParameterizedThreadStart(calculateRows));
			threads[i].Start(new TwoInts(i*rowPerThread, Math.Min((i+1)*rowPerThread, m1.rowsCount)));
		}
		for (int n = 0; n < threadCount; n++)
		{
			threads[n].Join();
		}
	}

	private void calculateRow(object? _row)
	{
		int row = (int)(_row ?? 0);
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
	private void calculateRows(object? _row)
	{
		int rowStart = ((TwoInts) (_row ?? new TwoInts(0,0))).num1;
		int rowEnd = ((TwoInts) (_row ?? new TwoInts(0, 0))).num2;
		for (int row = rowStart; row < rowEnd; row++)
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
		}
	}
}
