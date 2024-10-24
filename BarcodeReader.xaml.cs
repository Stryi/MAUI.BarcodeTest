using Microsoft.Maui.Storage;
using ZXing.Net.Maui.Controls;
using ZXing.QrCode.Internal;

namespace MauiApp2;

using VorratsUebersicht;

public partial class BarcodeReader : ContentPage
{
    public BarcodeReader()
    {
        InitializeComponent();
    }

    protected void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        try
        {
            // Handle barcode detected event here
            var barcodes = e?.Results; // Access the detected barcodes here
            if (barcodes == null)
            {
                LabelEAN.Text = $"e.Result ist NULL";
                return;
            }

            foreach (var barcode in barcodes)
            {
                LabelEAN.Text = $"Detected Barcode: {barcode?.Value}";
            }
        }
        catch(Exception ex)
        {
            LabelEAN.Text = ex.Message;
        }
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // Beispiel: "/data/user/0/de.stryi.Vorratsuebersicht/files"
		//string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.LocalApplicationData);

        var stream = await FileSystem.OpenAppPackageFileAsync("Vorraete_Demo.db3");
        
        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "Vorraete_db0.db3");

        {
            // Copy the file to the AppDataDirectory
            using FileStream outputStream = File.Create(targetFile);
            await stream.CopyToAsync(outputStream);

        }

        var conn = new SQLite.SQLiteConnection(targetFile, false);

        var article = conn.Query<Article>("SELECT ArticleId, Name FROM Article");
        if (article.Count > 0)
        {
            LabelEAN.Text = $"Detected Barcode: {article[0].Name}";
        }
        else
        {
            LabelEAN.Text = $"Detected Barcode: Keine Artikel";
        }
    }

    public async Task CopyFileToAppDataDirectory(string filename)
    {
        var stream = await FileSystem.OpenAppPackageFileAsync(filename);

        // Open the source file
        using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);

        // Create an output filename
        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filename);

        // Copy the file to the AppDataDirectory
        using FileStream outputStream = File.Create(targetFile);
        await inputStream.CopyToAsync(outputStream);
    }
}

