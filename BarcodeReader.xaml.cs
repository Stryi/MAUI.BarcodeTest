using ZXing.Net.Maui.Controls;
using ZXing.QrCode.Internal;

namespace MauiApp2;

public partial class BarcodeReader : ContentPage
{
    public BarcodeReader()
    {
        InitializeComponent();
    }

    protected void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        // Handle barcode detected event here
        var barcodes = e.Results; // Access the detected barcodes here
        foreach (var barcode in barcodes)
        {
            LabelEAN.Text = $"Detected Barcode: {barcode.Value}";
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        LabelEAN.Text = $"Detected Barcode: 0000000000000";
    }
}