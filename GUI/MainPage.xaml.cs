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

		ImageProcessor imageProcessor = new(img);

		ParallelOptions options = new() { MaxDegreeOfParallelism = 4 };
		Parallel.For(0, 4, options, operation => {
			if (operation == 0)
			{
				imageProcessor.Negative();
				MainThread.InvokeOnMainThreadAsync(() => {
					TopLeftImage.Source = (SKBitmapImageSource)imageProcessor.negative;
				});
			}
			if (operation == 1)
			{
				imageProcessor.GrayScale();
				MainThread.InvokeOnMainThreadAsync(() => {
					BottomLeftImage.Source = (SKBitmapImageSource)imageProcessor.grayScale;
				});
			}
			if (operation == 2)
			{
				imageProcessor.GaussianBlur();
				MainThread.InvokeOnMainThreadAsync(() =>
				{
					TopRightImage.Source = (SKBitmapImageSource)imageProcessor.gaussianBlur;
				});
			}
			if (operation == 3)
			{
				imageProcessor.LaplaceFilter();
				MainThread.InvokeOnMainThreadAsync(() =>
				{
					BottomRightImage.Source = (SKBitmapImageSource)imageProcessor.laplaceFilter;
				});
			}
		});

	}
}
