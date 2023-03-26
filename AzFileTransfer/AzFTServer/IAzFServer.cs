using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AzFTServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAzFServer”。
    [ServiceContract]
    public interface IAzFServer
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<FileInfo> GetAllFiles();

        [OperationContract]
        String SayHello();

        [OperationContract]
        Stream DownloadStream(FileInfo fileinfo);
    }
}
