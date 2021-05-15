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
using System.IO.Compression;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;

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
        private string _wowPath, _wtfPath, _intPath, _wtfZip, _intZip;

        static string[] _scopes = { DriveService.Scope.Drive,
                                    DriveService.Scope.DriveAppdata,
                                    DriveService.Scope.DriveFile,
                                    DriveService.Scope.DriveMetadataReadonly,
                                    DriveService.Scope.DriveReadonly,
                                    DriveService.Scope.DriveScripts};
        static string _applicationName = "World of Warcraft AddonsUploader";
        UserCredential _credential;
        DriveService _service;


        public MainWindow()
        {
            InitializeComponent();
            InitializeElements();
            if (System.IO.File.Exists(_settingsFullPath))
            {
                _wowPath = System.IO.File.ReadAllText(_settingsFullPath);
                _wtfZip = _wowPath + "\\wtf_temp.zip";
                _intZip = _wowPath + "\\int_temp.zip";
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
            if (System.IO.File.Exists(_settingsFullPath))
            {
                _dialog = new CommonOpenFileDialog()
                {
                    InitialDirectory = System.IO.File.ReadAllText(_settingsFullPath),
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

        private void GoogleLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var stream =
                new FileStream("client_cred.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);

            }


            // Create Drive API service.
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });
        }

        private void UIUpload_Click(object sender, RoutedEventArgs e)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "test.txt"
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(@"E:\settings.txt", System.IO.FileMode.Open))
            {
                request = _service.Files.Create(fileMetadata, stream, "text file/txt");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);
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
            System.IO.File.WriteAllText(_settingsFolder + _settingsName, _dialog.FileName);
        }

        private void ZipInterface_Click(object sender, RoutedEventArgs e)
        {
            if (WTFCheck.IsChecked == true && InterfaceCheck.IsChecked == true)
            {
                ZipFile.CreateFromDirectory(_wtfPath, _wtfZip);
                //ZipFile.CreateFromDirectory(_intPath, _intZip);
                MessageBox.Show("DONE");
            }
            else
            {
                MessageBox.Show("Wrong Directory");
            }
        }

       















        private void InterfaceCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void WTFCheck_Checked(object sender, RoutedEventArgs e)
        {

        }
 
        
    }
}

