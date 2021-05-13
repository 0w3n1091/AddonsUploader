using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;


namespace AddonsUploader
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CommonOpenFileDialog _dialog;
        private static readonly string _settingsFolder = "E:\\", _settingsName = "\\settings.txt";
        private static readonly string _settingsFullPath = _settingsFolder + _settingsName;
        private string _wtfPath;
        private string _intPath;
        private string _wowPath;

        

        public MainWindow()
        {
            InitializeComponent();
            InitializeElements();
            if (File.Exists(_settingsFullPath))
            {
                _wowPath = File.ReadAllText(_settingsFullPath);
                _wtfPath = _wowPath + "\\WTF";
                _intPath = _wowPath + "\\Interface";
                PathLabel.Content = _wowPath;
                FolderCheck();
            }
        }

        private void InitializeElements()
        {
            InterfaceCheck.IsEnabled = false;
            WTFCheck.IsEnabled = false;
            if (File.Exists(_settingsFullPath))
            {
                _dialog = new CommonOpenFileDialog()
                {
                    InitialDirectory = File.ReadAllText(_settingsFullPath),
                    IsFolderPicker = true,
                };
            }
            else
            {
                _dialog = new CommonOpenFileDialog()
                {
                    InitialDirectory = "C:\\",
                    IsFolderPicker = true
                };
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PathLabel.Content = _dialog.FileName;
                _wtfPath = _dialog.FileName + "\\WTF";
                _intPath = _dialog.FileName + "\\Interface";
                FolderCheck();
                SettingsSave();
            }
        }

        private void FolderCheck()
        {
            if (Directory.Exists(_intPath))
            {
                InterfaceCheck.IsChecked = true;
            }
            if (Directory.Exists(_wtfPath))
            {
                WTFCheck.IsChecked = true;
            }
            else
            {
                InterfaceCheck.IsChecked = false;
                WTFCheck.IsChecked = false;
            }
        }

        private void SettingsSave()
        {
            File.WriteAllText(_settingsFolder + _settingsName,_dialog.FileName);
        }

        private void InterfaceCheck_Checked(object sender, RoutedEventArgs e)
        {
         
        }

        private void WTFCheck_Checked(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
