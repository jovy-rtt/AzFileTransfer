using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using AzFileTransfer.ServiceReference1;
using System.ServiceModel.Security;
using System.ServiceModel;
using Microsoft.Win32;
using System.Diagnostics;

namespace AzFileTransfer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //分块传输 64M一块
        private int blockSize = 64 * 1024 * 1024 ;

        string fsFileDownLoadPath = @"C:\Users\jovy-\source\FSFileDownLoad";

        public MainWindow()
        {
            InitializeComponent();
            ShowServerFileList();

        }


        public void ShowServerFileList()
        {
            //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => {
            //    if (sslPolicyErrors == SslPolicyErrors.None)
            //    {
            //        return true;
            //    }
            //    return false;
            //};

            using (Service1Client client = new Service1Client())
            {
                GetAllFilesRequest allFilesRequest = new GetAllFilesRequest();
                GetAllFilesResponse getAllFilesResponse = new GetAllFilesResponse();
                getAllFilesResponse = client.GetAllFiles(allFilesRequest);
                FileInfo[] files = getAllFilesResponse.GetAllFilesResult;
                ServerFileList.ItemsSource = files;
            }
            //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            //client.ClientCredentials.ClientCertificate.SetCertificate(
            //    StoreLocation.LocalMachine,
            //    StoreName.My,
            //    X509FindType.FindBySubjectName,
            //    "AZC-cert"
            //    );
        }


        private async void btnDownload_click(object sender, RoutedEventArgs e)
        {

            FileInfo fiSelectedFile = ServerFileList.SelectedItem as FileInfo;
            long lstartPosition = 0;
            if (fiSelectedFile != null)
            {
                using (var client = new Service1Client())
                {
                    MesgInfo mesgInfo = new MesgInfo();
                    mesgInfo.sFilePathInfo = fiSelectedFile.FullName;
                    mesgInfo.lFileSize = fiSelectedFile.Length;

                    while (lstartPosition < fiSelectedFile.Length)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        FileDownLoadMessage request = new FileDownLoadMessage()
                        {
                            fiFileInfo = fiSelectedFile,
                            lFileSize = blockSize,
                            lOffset = lstartPosition,
                            abtFileData = new byte[blockSize]
                        };
                        FileDownLoadMessage response = new FileDownLoadMessage();
                        response = await client.DownLoadFileAsync(request);

                        using (var fileStream = new FileStream(System.IO.Path.Combine(fsFileDownLoadPath, response.fiFileInfo.Name), FileMode.Append, FileAccess.Write))
                        {
                            await fileStream.WriteAsync(response.abtFileData, 0, (int)response.lFileSize);
                        }
                        sw.Stop();
                        lstartPosition = response.lOffset;
                        mesgInfo.sFileStates = "正在下载数据...";
                        mesgInfo.lCurrent = lstartPosition;
                        mesgInfo.dSpeed = (double)response.lFileSize / sw.Elapsed.TotalSeconds / (1024 * 1024);
                        mesgInfo.lTime = (long)((fiSelectedFile.Length - lstartPosition) / (mesgInfo.dSpeed * (1024 * 1024)));
                        mesgInfo.sCanGO = "否";
                        updateMesgInfo(mesgInfo);
                    }
                    mesgInfo.sFileStates = "下载完毕！";
                    updateMesgInfo(mesgInfo);
                }
            }
            
        }

        private async void btnUpload_click(object sender, RoutedEventArgs e)
        {
            string sFilePath = tbFilePathInfo.Text;

            FileInfo fiSelectFile = null;
            fiSelectFile = new FileInfo(sFilePath);

            if (fiSelectFile != null)
            {
                MesgInfo mesgInfo = new MesgInfo();
                mesgInfo.sFilePathInfo = fiSelectFile.FullName;
                mesgInfo.lFileSize = fiSelectFile.Length;
                using (FileStream fileStream = new FileStream(fiSelectFile.FullName,FileMode.Open,FileAccess.Read))
                {
                    byte[] buf = new byte[blockSize];
                    long byteLeft = fiSelectFile.Length;
                    int bytesRead;
                    using (Service1Client client = new Service1Client())
                    {
                        while (byteLeft > 0)
                        {
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            bytesRead = await fileStream.ReadAsync(buf, 0, buf.Length);
                            byteLeft -= bytesRead;

                            FileUploadMessage message = new FileUploadMessage()
                            {
                                FileName = fiSelectFile.Name,
                                TotalFileSize = fiSelectFile.Length,
                                CurrentFileSize = fiSelectFile.Length - byteLeft,
                                FileData = new byte[bytesRead]
                            };

                            Array.Copy(buf, message.FileData, bytesRead);
                            await client.UploadFileAsync(message);
                            sw.Stop();
                            mesgInfo.sFileStates = "正在上传数据...";
                            mesgInfo.lCurrent = fiSelectFile.Length - byteLeft;
                            mesgInfo.dSpeed = (double)bytesRead / sw.Elapsed.TotalSeconds /(1024*1024);
                            mesgInfo.lTime = (long)((byteLeft ) / (mesgInfo.dSpeed * (1024 * 1024)) );
                            mesgInfo.sCanGO = "否";
                            updateMesgInfo(mesgInfo);
                        }
                    }
                }
                mesgInfo.sFileStates = "上传完毕！";
                updateMesgInfo(mesgInfo);
                ShowServerFileList();
            }
            else
            {
                MessageBox.Show("请先选择需要上传的文件！");
            }
        }

        // 选择上传
        private void btnSelectUpLoad_click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter and initial directory
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Show the dialog and get result
            bool? result = openFileDialog.ShowDialog();

            string sFilePath = "";
            if (result == true)
            {
                // Get selected file path
                sFilePath = openFileDialog.FileName;

                // Create FileInfo object for selected file
            }

            tbFilePathInfo.Text = sFilePath;
        }

        //选择改变，修改文件路径信息
        private void dgSC_click(object sender, SelectionChangedEventArgs e)
        {
            FileInfo fiSelectFileInfo = ServerFileList.SelectedItem as FileInfo;
            if (fiSelectFileInfo != null)
            {
                tbFileStates.Text = "准备就绪！";
                tbFileSize.Text = FormatFileSize(fiSelectFileInfo.Length);

                tbFilePathInfo.Text = "AzFTServer:\\";
                tbFilePathInfo.Text +=  fiSelectFileInfo.Name;
                ProBar.Value = 0;
            }
        }

        private async void updateMesgInfo(MesgInfo mesgInfo)
        {
            try
            {
                await Task.Run(() => {
                    Application.Current.Dispatcher.Invoke(() => {
                        tbFilePathInfo.Text = mesgInfo.sFilePathInfo;
                        tbFileStates.Text = mesgInfo.sFileStates;
                        tbFileSize.Text = FormatFileSize(mesgInfo.lFileSize);
                        double res = (double)mesgInfo.lCurrent / mesgInfo.lFileSize ;
                        string percent = $"{res * 100:F2}%";
                        tbCurrent.Text = FormatFileSize(mesgInfo.lCurrent) + "（"+ percent+"）";
                        tbSpeed.Text = mesgInfo.dSpeed.ToString() + " MB/秒";
                        tbTime.Text = mesgInfo.lTime.ToString() + " 秒";
                        tbCanGO.Text = mesgInfo.sCanGO.ToString();
                        ProBar.Value = (res * 100 );
                        tbMesg.Text = mesgInfo.sMesg;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static string FormatFileSize(long fileSizeInBytes)
        {
            const int scale = 1024;
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            int unitIndex = 0;

            double fileSize = fileSizeInBytes;
            while (fileSize >= scale && unitIndex < units.Length - 1)
            {
                fileSize /= scale;
                unitIndex++;
            }

            return $"{fileSize:0.#} {units[unitIndex]}";
        }
    }
}
