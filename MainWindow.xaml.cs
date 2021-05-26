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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.ComponentModel;
using Ionic.Zip;

namespace AddonsUploader
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CommonOpenFileDialog _dialog;
        private static readonly string _settingsFolder = AppDomain.CurrentDomain.BaseDirectory, _settingsName = "\\settings.txt";
        private static readonly string _settingsFullPath = _settingsFolder + _settingsName;
        private string _wtfPath, _intPath, _date, _folderID, _backupDirectory;
        private readonly string _wowPath, _intZip, _credPath = "token.json";
        private bool _driveCheck;
        private long _fileSize;
        static readonly string[] _scopes = { DriveService.Scope.Drive,
                                    DriveService.Scope.DriveAppdata,
                                    DriveService.Scope.DriveFile,
                                    DriveService.Scope.DriveMetadataReadonly,
                                    DriveService.Scope.DriveReadonly,
                                    DriveService.Scope.DriveScripts};
        private static readonly string _applicationName = "World of Warcraft InterfaceUploader";
        private UserCredential _credential;
        private DriveService _service;

        public class InterfaceElement
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string Size { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeElements();
            if (System.IO.Directory.Exists(_credPath))
            {
                GoogleAuthenticate();
            }
            else
            {
                OnlineStatus.Foreground = Brushes.Red;
                OnlineStatus.Content = "Disconnected";
            }
            if (System.IO.File.Exists(_settingsFullPath))
            {
                _wowPath = System.IO.File.ReadAllText(_settingsFullPath);
                _intZip = _wowPath + "\\int_temp.zip";
                _wtfPath = _wowPath + "\\WTF";
                _intPath = _wowPath + "\\Interface";
                PathLabel.Text = _wowPath;
                FolderCheck();
            }
            DriveList();
            if (_driveCheck == true)
            {
                InterfaceData.ItemsSource = ListInterface();
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
                PathLabel.Text = _dialog.FileName;
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
            System.IO.File.WriteAllText(_settingsFolder + _settingsName, _dialog.FileName);
        }

        private void GoogleLogin_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.Exists(_credPath))
            {
                System.IO.Directory.Delete(_credPath, true);
                OnlineStatus.Foreground = Brushes.Red;
                OnlineStatus.Content = "Disconnected";
                GoogleAuthenticate();
            }
            else
            {
                GoogleAuthenticate();
            }
        }

        private void GoogleAuthenticate()
        {
            using (var stream = new FileStream("client_cred.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.

                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(_credPath, true)).Result;
                OnlineStatus.Foreground = Brushes.Green;
                OnlineStatus.Content = "Connected";
            }
            // Create Drive API service.
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });
        }

        private void ZipInterface_Click(object sender, RoutedEventArgs e)
        {
            if (WTFCheck.IsChecked == true && InterfaceCheck.IsChecked == true)
            {
                if (System.IO.File.Exists(_intZip))
                {
                    MessageBox.Show("Interface archive already exists.");
                }

                else
                {
                    ZipIt();
                }
            }
            else
            {
                MessageBox.Show("Wrong Directory. Choose your Interface directory first.");
            }
        }

        private async void ZipIt()
        {
            BlockUI();
            await Task.Run(() =>
            {
                using (var zipFile = new ZipFile())
                {
                    zipFile.AddDirectory(_wtfPath, "WTF");
                    zipFile.AddDirectory(_intPath, "Interface");
                    zipFile.SaveProgress += (o, progress) =>
                    {
                        if (progress.EntriesSaved > 0 && progress.EntriesTotal > 0)
                        {
                            var percentage = (int)Math.Floor(progress.EntriesSaved * 100.0d / progress.EntriesTotal);
                            ProgressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action
                            (delegate ()
                            {
                                ProgressBar.Value = percentage;
                            }
                            ));
                        }
                    };
                    zipFile.Save(_intZip);
                }
            });
            MessageBox.Show("Creating Interface archive completed.");
            EnableUI();
        }

        private void UIUpload_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(_intZip))
            {
                _date = DateTime.Now.ToString();
                DriveList();
                ///////create folder///////////////////////////////////////////////
                var folderMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = _applicationName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                if (_driveCheck == false)
                {
                    FilesResource.CreateRequest folderRequest;
                    folderRequest = _service.Files.Create(folderMetaData);
                    folderRequest.Fields = "id";
                    var folder = folderRequest.Execute();
                    _folderID = folder.Id;
                }
                UploadIt();
            }
            else
            {
                MessageBox.Show("Interface archive missing. Zip Interface first.");
            }

        }

        private async void UploadIt()
        {
            BlockUI();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Parents = new List<string> { _folderID },
                Name = _date + ".zip",
            };
            var wtf = new System.IO.FileInfo(_intZip);
            FilesResource.CreateMediaUpload fileRequest;
            var stream = new System.IO.FileStream(_intZip, System.IO.FileMode.Open);
            fileRequest = _service.Files.Create(fileMetadata, stream, "zip file/.zip");
            fileRequest.Fields = "id";
            fileRequest.ChunkSize = FilesResource.CreateMediaUpload.MinimumChunkSize;
            fileRequest.ProgressChanged += (args) =>
            {
                if (args.BytesSent > 0 && wtf.Length > 0)
                {
                    var percentage = (int)Math.Floor(args.BytesSent * 100.0d / wtf.Length);
                    ProgressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action
                    (delegate ()
                    {
                        ProgressBar.Value = percentage;
                    }
                    ));
                }
            };
            await fileRequest.UploadAsync();
            InterfaceData.ItemsSource = ListInterface();
            stream.Close();
            MessageBox.Show("Upload completed");
            System.IO.File.Delete(_intZip);
            EnableUI();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (WTFCheck.IsChecked == true && InterfaceCheck.IsChecked == true)
            {
                if (System.IO.File.Exists(_intZip))
                {
                    MessageBox.Show("Restore File already exists. Delete it first.");
                }
                else
                {
                    BackupInterface();
                    _date = DateTime.Now.ToString("MM/dd/yyyy HH_mm_ss");
                    System.IO.Directory.CreateDirectory(_wtfPath);
                    object fileID = ((Button)sender).CommandParameter;
                    RestoreIt(fileID);
                }
            }
            else
            {
                MessageBox.Show("Wrong Directory. Choose your Interface directory first.");
            }
        }
        private async void RestoreIt(object fileID)
        {
            BlockUI();
            FilesResource.ListRequest listRequest = _service.Files.List();
            string query = "'" + _folderID + "'" + " in parents";
            listRequest.Q = query;
            listRequest.PageSize = 999;
            listRequest.Fields = "nextPageToken, files(id, size)";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Id == fileID.ToString())
                    {
                        _fileSize = (long)file.Size;
                    }
                }
            }
            FilesResource.GetRequest downloadRequest;
            downloadRequest = _service.Files.Get(fileID.ToString());
            var stream = new System.IO.MemoryStream();
            downloadRequest.MediaDownloader.ChunkSize = FilesResource.CreateMediaUpload.MinimumChunkSize;
            downloadRequest.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                int percentage = (int)Math.Floor(progress.BytesDownloaded * 100.0d / _fileSize);
                ProgressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action
                (delegate ()
                {
                    ProgressBar.Value = percentage;
                }
                ));
            };
            await downloadRequest.DownloadAsync(stream);
            System.IO.FileStream saveFile = new System.IO.FileStream(_intZip, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            stream.WriteTo(saveFile);
            saveFile.Close();
            stream.Close();
            EnableUI();
            UnZipIt();
        }

        private async void UnZipIt()
        {
            BlockUI();
            await Task.Run(() =>
            {
                using (var zipFile = ZipFile.Read(_intZip))
                {
                    zipFile.ExtractProgress += (o, args) =>
                    {
                        if (args.EntriesExtracted > 0 && args.EntriesTotal > 0)
                        {
                            var percentage = (int)Math.Floor(args.EntriesExtracted * 100.0d / args.EntriesTotal);
                            ProgressBar.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action
                            (delegate ()
                            {
                                ProgressBar.Value = percentage;

                            }
                            ));
                        }
                    };
                    zipFile.ExtractAll(_wowPath);
                }
            });
            MessageBox.Show("Restoration completed.");
            System.IO.File.Delete(_intZip);
            EnableUI();
        }
    
        private void DriveList()
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.PageSize = 999;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Name == _applicationName)
                    {
                        _driveCheck = true;
                        _folderID = file.Id;
                    }
                }
            }
        }

        public List<InterfaceElement> ListInterface()
        {
            List<InterfaceElement> dataGrid = new List<InterfaceElement>();
            FilesResource.ListRequest listRequest = _service.Files.List();
            string query = "'" + _folderID + "'" + " in parents";
            listRequest.Q = query;
            listRequest.PageSize = 999;
            listRequest.Fields = "nextPageToken, files(id, name, size)";
            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {

                    dataGrid.Add(new InterfaceElement()
                    {
                        ID = file.Id,
                        Name = file.Name,
                        Size = ((long)file.Size) / 1048576 + " MB"
                    });
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            return dataGrid;
        }

        
        private void InterfaceLoad_Click(object sender, RoutedEventArgs e)
        {
            InterfaceData.ItemsSource = ListInterface();
        }

        private void BlockUI()
        {
            PathButton.IsEnabled = false;
            ZipInterface.IsEnabled = false;
            LoginButton.IsEnabled = false;
            UIUpload.IsEnabled = false;
            InterfaceList.IsEnabled = false;
            InterfaceData.IsEnabled = false;
        }

        private void EnableUI()
        {
            PathButton.IsEnabled = true;
            ZipInterface.IsEnabled = true;
            LoginButton.IsEnabled = true;
            UIUpload.IsEnabled = true;
            InterfaceList.IsEnabled = true;
            InterfaceData.IsEnabled = true;
        }

        private void BackupInterface()
        {
            if (System.IO.Directory.Exists(_wtfPath) && System.IO.Directory.Exists(_intPath))
            {
                _backupDirectory = _wowPath + "\\InterfaceBackup" + "\\" + DateTime.Now.ToString("MM/dd/yyyyHH_mm_ss");
                System.IO.Directory.CreateDirectory(_backupDirectory);
                System.IO.Directory.Move(_intPath, _backupDirectory + "\\Interface");
                System.IO.Directory.Move(_wtfPath, _backupDirectory + "\\WTF");
            }
            else
            {
                MessageBox.Show("Interface directories missing.");
            }
        }
    }
}

