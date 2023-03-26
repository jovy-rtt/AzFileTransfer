using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AzFTServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        string fSFilePath = @"C:\Users\jovy-\source\FSFile";
        
        public List<FileInfo> GetAllFiles()
        {

            //string Uiddir = Path.Combine(MyPath, uid);
            string[] filePaths = Directory.GetFiles(fSFilePath);

            List<FileInfo> files = new List<FileInfo>();
            foreach (string fl in filePaths)
            {
                files.Add(new FileInfo(fl));
            }
            return files;
        }

        public Stream DownloadStream(FileInfo fileinfo)
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

        public async void UploadFile(FileUploadMessage fileUploadMessage)
        {
            string filePath = Path.Combine(fSFilePath,fileUploadMessage.FileName);
            using (FileStream fileStream = new FileStream(filePath,FileMode.Append))
            {
                await fileStream.WriteAsync(fileUploadMessage.FileData, 0, fileUploadMessage.FileData.Length);
            }
        }

        public FileDownLoadMessage DownLoadFile(FileDownLoadMessage fdmFDLM)
        {
            string sFilePath = fdmFDLM.fiFileInfo.FullName;
            using (FileStream fsFileStream = new FileStream(sFilePath,FileMode.Open,FileAccess.Read))
            {
                fsFileStream.Seek(fdmFDLM.lOffset, SeekOrigin.Begin);
                byte[] abtbuf = new byte[fdmFDLM.lFileSize];
                int bytesread =  fsFileStream.Read(abtbuf, 0, (int)fdmFDLM.lFileSize);
                FileDownLoadMessage fdm = new FileDownLoadMessage()
                {
                    fiFileInfo = new FileInfo(sFilePath),
                    lOffset = fsFileStream.Position,
                    lFileSize = bytesread,
                    abtFileData = new byte[bytesread]
                };
                Array.Copy(abtbuf, fdm.abtFileData, bytesread);
                return fdm;
            }
        }

    }
}
