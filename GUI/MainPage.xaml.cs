namespace GUI;
using System.Drawing;

using System;
using System.Drawing.Imaging;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public partial class MainPage : ContentPage
{

	//Bitmap? imageToProcess;
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
		// TopLeftImage.Source = null;
		// TopRightImage.Source = null;
		// BottomLeftImage.Source = null;
		// BottomRightImage.Source = null;

		for (int x = 0; x < img.Width; x++)
		{
			for (int y = 0; y < img.Height; y++)
			{
				var color = img.GetPixel(x,y);
				color = color.WithRed((byte) (Byte.MaxValue - color.Red));
				color = color.WithGreen((byte) (Byte.MaxValue - color.Green));
				color = color.WithBlue((byte)(Byte.MaxValue - color.Blue));
				img.SetPixel(x,y,color);
			}
		}

		SKBitmapImageSource source = img;
		//source.Bitmap = img;

		TopLeftImage.Source = source;
		TopRightImage.Source =  source;
		BottomLeftImage.Source =  source;
		BottomRightImage.Source =  source;

	}
}

