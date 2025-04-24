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
				var image = ImageSource.FromStream(() => stream);
				MainImage.Source = image;
				TopLeftImage.Source = null;
				TopRightImage.Source = null;
				BottomLeftImage.Source = null;
				BottomRightImage.Source = null;
				// imageToProcess = new Bitmap(result.FullPath);
				// img = new(result.FullPath);
				//SKImageInfo imgInfo = new();
				//SKBitmap.Decode(result.FullPath, imgInfo);
				img = SKBitmap.Decode(result.FullPath);
				// Console.WriteLine("...");
				// SKBitmapImageSource test = (SKBitmapImageSource) image;
				// Console.WriteLine("---");
				Console.WriteLine(img.IsEmpty);
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

		SKBitmapImageSource source = img;
		//source.Bitmap = img;

		TopLeftImage.Source = (ImageSource) source;
		TopRightImage.Source = (ImageSource) source;
		BottomLeftImage.Source = (ImageSource) source;
		BottomRightImage.Source = (ImageSource) source;

	}
}

