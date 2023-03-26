using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AzFTServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AzFServer”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AzFServer.svc 或 AzFServer.svc.cs，然后开始调试。
    public class AzFServer : IAzFServer
    {
        public void DoWork()
        {
        }

        public List<FileInfo> GetAllFiles()
        {

            string MyPath = @"C:\Users\jovy-\source\repos\AzFileTransfer\AzFTServer\UserFiles";
            //string Uiddir = Path.Combine(MyPath, uid);
            string[] filePaths = Directory.GetFiles(MyPath);

            List<FileInfo> files = new List<FileInfo>();
            foreach (string fl in filePaths)
            {
                files.Add(new FileInfo(fl));
            }
            return files;
        }

        Stream IAzFServer.DownloadStream(FileInfo fileinfo)
        {
            try
            {
                FileStream file = File.OpenRead(fileinfo.FullName);
                return file;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
        }

        string IAzFServer.SayHello()
        {
            return "hello";
        }
    }
}
