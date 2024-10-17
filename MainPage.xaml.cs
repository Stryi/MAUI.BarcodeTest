using ZXing;

namespace MAUI.BarcodeTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            /*
            barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
                AutoRotate = true,
                Multiple = true
            };
            */
        }

        protected void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            // Handle barcode detected event here
            var barcodes = e.Results; // Access the detected barcodes here
            foreach (var barcode in barcodes)
            {
                Console.WriteLine($"Detected Barcode: {barcode.Value}");
            }
        }

        // Event-Handler für den Button-Klick
        private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }
    }
}
