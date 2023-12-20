using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;

namespace BMSHPMS.DSReception.ViewModels
{
    public class DSRegResultVM
    {
        public string Message { get; set; }

        public List<DSDonorInfo> DonorInfos { get; set; } = new List<DSDonorInfo>();

        public List<DSLongevityInfo> LongevityInfos { get; set; } = new List<DSLongevityInfo>();

        public List<DSMemorialInfo> MemorialInfos { get; set; } = new List<DSMemorialInfo>();

        public void Sort()
        {
            DonorInfos?.Sort((x,y)=>x.SerialCode.CompareTo(y.SerialCode));
            LongevityInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            MemorialInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
        }
    }
}
