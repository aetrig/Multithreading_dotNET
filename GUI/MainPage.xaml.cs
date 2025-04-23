namespace GUI;
using System.Drawing;

using System;
using System.Drawing.Imaging;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public partial class MainPage : ContentPage
{

	Bitmap? imageToProcess;
	public MainPage()
	{
		InitializeComponent();
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
				imageToProcess = new Bitmap(result.FullPath);
			}
		}
		catch
		{
		}
	}
	private void ProcessClicked(object sender, EventArgs e)
	{
		Console.WriteLine("Process button pressed");
		TopLeftImage.Source = null;
		TopRightImage.Source = null;
		BottomLeftImage.Source = null;
		BottomRightImage.Source = null;

		if (imageToProcess != null)
		{
			string tempFile = "GUI/Resources/Images/temp.jpg";
			if (File.Exists(tempFile))
			{
				File.Delete(tempFile);
			}

			//stream.Flush();

			imageToProcess.Save(tempFile);
			var image = ImageSource.FromFile("temp.jpg");
			TopLeftImage.Source = image;
			;
			// stream.Flush();
			// imageToProcess.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			// TopRightImage.Source = ImageSource.FromStream(() => stream);

			// stream.Flush();
			// imageToProcess.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			// BottomLeftImage.Source = ImageSource.FromStream(() => stream);

			// stream.Flush();
			// imageToProcess.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			// BottomRightImage.Source = ImageSource.FromStream(() => stream);
		}

	}
}

