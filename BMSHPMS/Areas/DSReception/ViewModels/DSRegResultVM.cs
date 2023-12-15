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
    }
}
