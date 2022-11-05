using Avalonia.Controls;
using Avalonia.Interactivity;
using SVGMin.Models;

namespace SVGMin.Views
{
    public partial class MainWindow : Window
    {
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
                SVGFile svgFile = new SVGFile(fileSelected[0]);
                TxtSVG.Text = svgFile.ReadFile();
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
    }
}