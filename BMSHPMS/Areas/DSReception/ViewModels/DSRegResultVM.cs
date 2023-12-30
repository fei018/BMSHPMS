using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;

namespace BMSHPMS.DSReception.ViewModels
{
    public class DSRegResultVM
    {
        public string Message { get; set; }

        public string ReceiptNumber { get; set; }

        public List<Info_Donor> DonorInfos { get; set; } = new List<Info_Donor>();

        public List<Info_Longevity> LongevityInfos { get; set; } = new List<Info_Longevity>();

        public List<Info_Memorial> MemorialInfos { get; set; } = new List<Info_Memorial>();

        public void Sort()
        {
            DonorInfos?.Sort((x,y)=>x.SerialCode.CompareTo(y.SerialCode));
            LongevityInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            MemorialInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
        }
    }
}
