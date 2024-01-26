using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;

namespace BMSHPMS.DSReception.ViewModels
{
    public class DSRegResultVM
    {
        public string Message { get; set; }

        public string ReceiptNumber { get; set; }

        /// <summary>
        /// 收據所屬法會名
        /// </summary>
        public string DharmaServiceName { get; set; }

        /// <summary>
        /// 提交的法會ID
        /// </summary>
        public Guid? DharmaServiceID { get; set; }

        //public Opt_DharmaService DharmaService { get; set; }

        public List<Info_Donor> Donors { get; set; } = new List<Info_Donor>();

        public List<Info_Longevity> Longevitys { get; set; } = new List<Info_Longevity>();

        public List<Info_Memorial> Memorials { get; set; } = new List<Info_Memorial>();

        public void Sort()
        {
            Donors?.Sort((x,y)=>x.SerialCode.CompareTo(y.SerialCode));
            Longevitys?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            Memorials?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
        }
    }
}
