using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AzFTServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<FileInfo> GetAllFiles();

        [OperationContract]
        Stream DownloadStream(FileInfo fileinfo);

        [OperationContract]
        void UploadFile(FileUploadMessage fileUploadMessage);

        [OperationContract]
        FileDownLoadMessage DownLoadFile(FileDownLoadMessage fdmFDLM);

    }


    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader]
        public string FileName { get; set; }

        [MessageHeader]
        public long TotalFileSize { get; set; }

        [MessageHeader]
        public long CurrentFileSize { get; set; }

        [MessageBodyMember]
        public byte[] FileData { get; set; }
    }

    [MessageContract]
    public class FileDownLoadMessage
    {
        [MessageHeader]
        public FileInfo fiFileInfo { get; set; }
        [MessageHeader]
        public long lOffset { get; set; }
        [MessageHeader]
        public long lFileSize { get; set; }
        [MessageBodyMember]
        public byte[] abtFileData { get; set; }
    }
}
