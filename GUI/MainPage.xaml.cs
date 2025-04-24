namespace GUI;

using System;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp;

public partial class MainPage : ContentPage
{

	SKBitmap img;
	public MainPage()
	{
		InitializeComponent();
		img = new();
	}

	
	private async void FileSelectClicked(object sender, EventArgs e)
	{
		Console.WriteLine("File button pressed");
		PickOptions options = new();
		options.FileTypes = FilePickerFileType.Images;
		try
		{
			var result = await FilePicker.Default.PickAsync(options);
			if (result != null)
			{
				var stream = await result.OpenReadAsync();
				var imageSource = ImageSource.FromStream(() => stream);
				MainImage.Source = imageSource;
				TopLeftImage.Source = null;
				TopRightImage.Source = null;
				BottomLeftImage.Source = null;
				BottomRightImage.Source = null;

				img = SKBitmap.Decode(result.FullPath);
			}
		}
		catch
		{
		}
	}
	private void ProcessClicked(object sender, EventArgs e)
	{
		Console.WriteLine("Process button pressed");

		SKBitmap negative = img.Copy();
		SKBitmap grayScale = img.Copy();
		SKBitmap gaussianBlur = img.Copy();
		SKBitmap edges = img.Copy();

		for (int x = 0; x < negative.Width; x++)
		{
			for (int y = 0; y < negative.Height; y++)
			{
				var color = negative.GetPixel(x,y);
				color = color.WithRed((byte) (Byte.MaxValue - color.Red));
				color = color.WithGreen((byte) (Byte.MaxValue - color.Green));
				color = color.WithBlue((byte)(Byte.MaxValue - color.Blue));
				negative.SetPixel(x,y,color);
			}
		}

		for (int x = 0; x < grayScale.Width; x++)
		{
			for (int y = 0; y < grayScale.Height; y++)
			{
				var color = grayScale.GetPixel(x, y);
				var average = (color.Red + color.Green + color.Blue)/3;
				color = color.WithRed((byte)(average));
				color = color.WithGreen((byte)(average));
				color = color.WithBlue((byte)(average));
				grayScale.SetPixel(x, y, color);
			}
		}

		TopLeftImage.Source = (SKBitmapImageSource) negative;
		TopRightImage.Source =  (SKBitmapImageSource) gaussianBlur;
		BottomLeftImage.Source =  (SKBitmapImageSource) grayScale;
		BottomRightImage.Source =  (SKBitmapImageSource) edges;

	}
}

