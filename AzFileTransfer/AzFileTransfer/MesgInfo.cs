using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzFileTransfer
{
    public class MesgInfo
    {
        public string sFilePathInfo { get; set; }

        public string sFileStates { get; set; }
        public long lFileSize { get; set; }
        public long lCurrent { get; set; }
        public double dSpeed { get; set; }
        public long lTime { get; set; }

        public string sCanGO { get; set; }
        public int iProBar { get; set; }

        public string sMesg { get; set; }

        public MesgInfo() 
        { 
            this.sFilePathInfo = string.Empty;
            this.sFileStates = string.Empty;
            this.lFileSize = 0;
            this.lCurrent = 0;
            this.dSpeed = 0;
            this.lTime = 0;
            this.sCanGO = string.Empty;
            this.sMesg = string.Empty;
            this.iProBar = 0;
        }
    }
}
