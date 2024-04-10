using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    /// <summary>
    /// 延生 20格 205x254mm 紅紙 (100元)
    /// </summary>
    public class PrintPlaqueData_Donor_Long
    {
        #region 延生 string
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public string Name7 { get; set; }
        public string Name8 { get; set; }
        public string Name9 { get; set; }
        public string Name10 { get; set; }
        public string Name11 { get; set; }
        public string Name12 { get; set; }
        public string Name13 { get; set; }
        public string Name14 { get; set; }
        public string Name15 { get; set; }
        public string Name16 { get; set; }
        public string Name17 { get; set; }
        public string Name18 { get; set; }
        public string Name19 { get; set; }
        public string Name20 { get; set; }

        public string Serial1 { get; set; }
        public string Serial2 { get; set; }
        public string Serial3 { get; set; }
        public string Serial4 { get; set; }
        public string Serial5 { get; set; }
        public string Serial6 { get; set; }
        public string Serial7 { get; set; }
        public string Serial8 { get; set; }
        public string Serial9 { get; set; }
        public string Serial10 { get; set; }
        public string Serial11 { get; set; }
        public string Serial12 { get; set; }
        public string Serial13 { get; set; }
        public string Serial14 { get; set; }
        public string Serial15 { get; set; }
        public string Serial16 { get; set; }
        public string Serial17 { get; set; }
        public string Serial18 { get; set; }
        public string Serial19 { get; set; }
        public string Serial20 { get; set; }
        #endregion

        public PrintPlaqueData_Donor_Long(List<Info_Donor> list)
        {
            #region 填充數據
            Name1 = list.ElementAtOrDefault(0) != null ? list[0].LongevityName : "增福延壽";
            Name2 = list.ElementAtOrDefault(1) != null ? list[1].LongevityName : "增福延壽";
            Name3 = list.ElementAtOrDefault(2) != null ? list[2].LongevityName : "增福延壽";
            Name4 = list.ElementAtOrDefault(3) != null ? list[3].LongevityName : "增福延壽";
            Name5 = list.ElementAtOrDefault(4) != null ? list[4].LongevityName : "增福延壽";
            Name6 = list.ElementAtOrDefault(5) != null ? list[5].LongevityName : "增福延壽";
            Name7 = list.ElementAtOrDefault(6) != null ? list[6].LongevityName : "增福延壽";
            Name8 = list.ElementAtOrDefault(7) != null ? list[7].LongevityName : "增福延壽";
            Name9 = list.ElementAtOrDefault(8) != null ? list[8].LongevityName : "增福延壽";
            Name10 = list.ElementAtOrDefault(9) != null ? list[9].LongevityName : "增福延壽";
            Name11 = list.ElementAtOrDefault(10) != null ? list[10].LongevityName : "增福延壽";
            Name12 = list.ElementAtOrDefault(11) != null ? list[11].LongevityName : "增福延壽";
            Name13 = list.ElementAtOrDefault(12) != null ? list[12].LongevityName : "增福延壽";
            Name14 = list.ElementAtOrDefault(13) != null ? list[13].LongevityName : "增福延壽";
            Name15 = list.ElementAtOrDefault(14) != null ? list[14].LongevityName : "增福延壽";
            Name16 = list.ElementAtOrDefault(15) != null ? list[15].LongevityName : "增福延壽";
            Name17 = list.ElementAtOrDefault(16) != null ? list[16].LongevityName : "增福延壽";
            Name18 = list.ElementAtOrDefault(17) != null ? list[17].LongevityName : "增福延壽";
            Name19 = list.ElementAtOrDefault(18) != null ? list[18].LongevityName : "增福延壽";
            Name20 = list.ElementAtOrDefault(19) != null ? list[19].LongevityName : "增福延壽";

            Serial1 = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";
            Serial2 = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";
            Serial3 = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";
            Serial4 = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";
            Serial5 = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";
            Serial6 = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";
            Serial7 = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";
            Serial8 = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";
            Serial9 = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";
            Serial10 = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            Serial11 = list.ElementAtOrDefault(10) != null ? list[10].SerialCode : "";
            Serial12 = list.ElementAtOrDefault(11) != null ? list[11].SerialCode : "";
            Serial13 = list.ElementAtOrDefault(12) != null ? list[12].SerialCode : "";
            Serial14 = list.ElementAtOrDefault(13) != null ? list[13].SerialCode : "";
            Serial15 = list.ElementAtOrDefault(14) != null ? list[14].SerialCode : "";
            Serial16 = list.ElementAtOrDefault(15) != null ? list[15].SerialCode : "";
            Serial17 = list.ElementAtOrDefault(16) != null ? list[16].SerialCode : "";
            Serial18 = list.ElementAtOrDefault(17) != null ? list[17].SerialCode : "";
            Serial19 = list.ElementAtOrDefault(18) != null ? list[18].SerialCode : "";
            Serial20 = list.ElementAtOrDefault(19) != null ? list[19].SerialCode : "";
            #endregion

        }
    }
}
