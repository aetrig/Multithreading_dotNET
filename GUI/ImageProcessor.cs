using System;
using SkiaSharp;

namespace GUI;

public class ImageProcessor
{
	public SKBitmap sourceImg { get; set; }
	public SKBitmap negative { get; private set; }
	public SKBitmap grayScale { get; private set; }
	public SKBitmap gaussianBlur { get; private set; }
	public SKBitmap laplaceFilter { get; private set; }

	public ImageProcessor(SKBitmap img)
	{
		sourceImg = img.Copy();
		negative = img.Copy();
		grayScale = img.Copy();
		gaussianBlur = img.Copy();
		laplaceFilter = img.Copy();
	}

	public void Negative()
	{
		
		for (int x = 0; x < sourceImg.Width; x++)
		{
			for (int y = 0; y < sourceImg.Height; y++)
			{
				var color = sourceImg.GetPixel(x, y);
				color = color.WithRed((byte)(Byte.MaxValue - color.Red));
				color = color.WithGreen((byte)(Byte.MaxValue - color.Green));
				color = color.WithBlue((byte)(Byte.MaxValue - color.Blue));
				negative.SetPixel(x, y, color);
			}
		}
	}

	public void GrayScale()
	{
		for (int x = 0; x < sourceImg.Width; x++)
		{
			for (int y = 0; y < sourceImg.Height; y++)
			{
				var color = sourceImg.GetPixel(x, y);
				var average = (color.Red + color.Green + color.Blue) / 3;
				color = color.WithRed((byte)(average));
				color = color.WithGreen((byte)(average));
				color = color.WithBlue((byte)(average));
				grayScale.SetPixel(x, y, color);
			}
		}
	}

	public void GaussianBlur()
	{
		int[,] convolutionMatrix = new int[5,5]{{1,2,4,2,1},{2,4,8,4,2},{4,8,16,8,4},{2,4,8,4,2},{1,2,4,2,1}};
		for (int x = 2; x < sourceImg.Width-2; x++)
		{
			for (int y = 2; y < sourceImg.Height-2; y++)
			{
				SKColor color = sourceImg.GetPixel(x, y);
				SKColor[,] colors = new SKColor[5,5];
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						colors[i,j] = sourceImg.GetPixel(x+i-2,y+j-2);
					}
				}

				int[,] reds = new int[5,5];
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						reds[i, j] = colors[i, j].Red * convolutionMatrix[i, j];
					}
				}
				int Red = 0;
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						Red += reds[i, j];
					}
				}

				int[,] greens = new int[5, 5];
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						greens[i, j] = colors[i, j].Green * convolutionMatrix[i, j];
					}
				}
				int Green = 0;
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						Green += greens[i, j];
					}
				}

				int[,] blues = new int[5, 5];
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						blues[i, j] = colors[i, j].Blue * convolutionMatrix[i, j];
					}
				}
				int Blue = 0;
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						Blue += blues[i, j];
					}
				}

				color = color.WithRed((byte)(Red/100));
				color = color.WithGreen((byte)(Green/100));
				color = color.WithBlue((byte)(Blue/100));
				gaussianBlur.SetPixel(x, y, color);
			}
		}
	}

	public void LaplaceFilter()
	{
		GrayScale();

		int[,] convolutionMatrix = new int[3, 3] { {-1,-1,-1}, {-1,8,-1}, {-1,-1,-1} };
		for (int x = 1; x < grayScale.Width - 1; x++)
		{
			for (int y = 1; y < grayScale.Height - 1; y++)
			{
				SKColor color = grayScale.GetPixel(x, y);
				SKColor[,] colors = new SKColor[3, 3];
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						colors[i, j] = grayScale.GetPixel(x + i - 1, y + j - 1);
					}
				}

				int[,] grays = new int[3, 3];
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						grays[i, j] = colors[i, j].Red * convolutionMatrix[i, j];
					}
				}
				int Gray = 0;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						Gray += grays[i, j];
					}
				}

				color = color.WithRed((byte)(Gray));
				color = color.WithGreen((byte)(Gray));
				color = color.WithBlue((byte)(Gray));
				laplaceFilter.SetPixel(x, y, color);
			}
		}
	}
	
}
