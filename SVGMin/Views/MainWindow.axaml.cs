using Avalonia.Controls;
using Avalonia.Interactivity;
using SVGMin.Models;

namespace SVGMin.Views
{
    public partial class MainWindow : Window
    {
        private bool ShowMin = false;
        private SVGFile _svgFile;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnFileOpen(object? sender, RoutedEventArgs e)
        {
            var window = new Window();
            
            /*
            var sp = new ManagedStorageProvider<Window>(window,null);
            var picked = await sp.OpenFilePickerAsync(new FilePickerOpenOptions());
            var s = picked[0].Name;
            LblFileName.Content = s;
            */
            
            var fileDialog = new OpenFileDialog();
            var fileSelected = await fileDialog.ShowAsync(window);
            if (fileSelected != null)
            {
                TxtFileName.Text = fileSelected[0];
                _svgFile = new SVGFile(fileSelected[0]);
                TxtSVG.Text = _svgFile.ReadFile();
                _svgFile.Minify();
            }                
        }

        private async void BtnFileSave(object? sender, RoutedEventArgs e)
        {
            var window = new Window();
            var fileDialog = new SaveFileDialog();
            var fileSelected = await fileDialog.ShowAsync(window);
            if (fileSelected != null)
            {
                TxtFileName.Text = "Saving: "+fileSelected;
            }  
        }

        private void BtnMinOrig(object? sender, RoutedEventArgs e)
        {
            ShowMin = !ShowMin;
            if (ShowMin)
            {
                TxtSVG.Text = _svgFile.Minified;
                BtnMO.Content = "Show Original";
            }
            else
            {
                TxtSVG.Text = _svgFile.Contents;
                BtnMO.Content = "Show Minified";
            }
        }
    }
}