using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    public class PrintPlaqueData_Memorial
    {
        #region string
        public string ADece1 { get; set; }
        public string ADece2 { get; set; }
        public string ADece3 { get; set; }
        public string ABene1 { get; set; }
        public string ASerial { get; set; }


        public string BDece1 { get; set; }
        public string BDece2 { get; set; }
        public string BDece3 { get; set; }
        public string BBene1 { get; set; }
        public string BSerial { get; set; }

        public string CDece1 { get; set; }
        public string CDece2 { get; set; }
        public string CDece3 { get; set; }
        public string CBene1 { get; set; }
        public string CSerial { get; set; }

        public string DDece1 { get; set; }
        public string DDece2 { get; set; }
        public string DDece3 { get; set; }
        public string DBene1 { get; set; }
        public string DSerial { get; set; }

        public string EDece1 { get; set; }
        public string EDece2 { get; set; }
        public string EDece3 { get; set; }
        public string EBene1 { get; set; }
        public string ESerial { get; set; }

        public string FDece1 { get; set; }
        public string FDece2 { get; set; }
        public string FDece3 { get; set; }
        public string FBene1 { get; set; }
        public string FSerial { get; set; }

        public string GDece1 { get; set; }
        public string GDece2 { get; set; }
        public string GDece3 { get; set; }
        public string GBene1 { get; set; }
        public string GSerial { get; set; }

        public string HDece1 { get; set; }
        public string HDece2 { get; set; }
        public string HDece3 { get; set; }
        public string HBene1 { get; set; }
        public string HSerial { get; set; }

        public string IDece1 { get; set; }
        public string IDece2 { get; set; }
        public string IDece3 { get; set; }
        public string IBene1 { get; set; }
        public string ISerial { get; set; }

        public string JDece1 { get; set; }
        public string JDece2 { get; set; }
        public string JDece3 { get; set; }
        public string JBene1 { get; set; }
        public string JSerial { get; set; }

        //------------------------

        public string A2Dece1 { get; set; }
        public string A2Dece2 { get; set; }
        public string A2Dece3 { get; set; }
        public string A2Bene1 { get; set; }
        public string A2Serial { get; set; }
                       
        public string B2Dece1 { get; set; }
        public string B2Dece2 { get; set; }
        public string B2Dece3 { get; set; }
        public string B2Bene1 { get; set; }
        public string B2Serial { get; set; }
                       
        public string C2Dece1 { get; set; }
        public string C2Dece2 { get; set; }
        public string C2Dece3 { get; set; }
        public string C2Bene1 { get; set; }
        public string C2Serial { get; set; }
                       
        public string D2Dece1 { get; set; }
        public string D2Dece2 { get; set; }
        public string D2Dece3 { get; set; }
        public string D2Bene1 { get; set; }
        public string D2Serial { get; set; }
                       
        public string E2Dece1 { get; set; }
        public string E2Dece2 { get; set; }
        public string E2Dece3 { get; set; }
        public string E2Bene1 { get; set; }
        public string E2Serial { get; set; }
                       
        public string F2Dece1 { get; set; }
        public string F2Dece2 { get; set; }
        public string F2Dece3 { get; set; }
        public string F2Bene1 { get; set; }
        public string F2Serial { get; set; }
                       
        public string G2Dece1 { get; set; }
        public string G2Dece2 { get; set; }
        public string G2Dece3 { get; set; }
        public string G2Bene1 { get; set; }
        public string G2Serial { get; set; }
                       
        public string H2Dece1 { get; set; }
        public string H2Dece2 { get; set; }
        public string H2Dece3 { get; set; }
        public string H2Bene1 { get; set; }
        public string H2Serial { get; set; }
                       
        public string I2Dece1 { get; set; }
        public string I2Dece2 { get; set; }
        public string I2Dece3 { get; set; }
        public string I2Bene1 { get; set; }
        public string I2Serial { get; set; }
                       
        public string J2Dece1 { get; set; }
        public string J2Dece2 { get; set; }
        public string J2Dece3 { get; set; }
        public string J2Bene1 { get; set; }
        public string J2Serial { get; set; }
        #endregion

        public PrintPlaqueData_Memorial(List<Info_Memorial> list)
        {
            #region 填充數據
            ABene1 = list.ElementAtOrDefault(0) != null ? list[0].BenefactorName : "";
            ADece1 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_1 : "";
            ADece2 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_2 : "";
            ADece3 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_3 : "";
            ASerial = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";

            BBene1 = list.ElementAtOrDefault(1) != null ? list[1].BenefactorName : "";
            BDece1 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_1 : "";
            BDece2 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_2 : "";
            BDece3 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_3 : "";
            BSerial = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";

            CBene1 = list.ElementAtOrDefault(2) != null ? list[2].BenefactorName : "";
            CDece1 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_1 : "";
            CDece2 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_2 : "";
            CDece3 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_3 : "";
            CSerial = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";

            DBene1 = list.ElementAtOrDefault(3) != null ? list[3].BenefactorName : "";
            DDece1 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_1 : "";
            DDece2 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_2 : "";
            DDece3 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_3 : "";
            DSerial = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";

            EBene1 = list.ElementAtOrDefault(4) != null ? list[4].BenefactorName : "";
            EDece1 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_1 : "";
            EDece2 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_2 : "";
            EDece3 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_3 : "";
            ESerial = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";

            FBene1 = list.ElementAtOrDefault(5) != null ? list[5].BenefactorName : "";
            FDece1 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_1 : "";
            FDece2 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_2 : "";
            FDece3 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_3 : "";
            FSerial = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";

            GBene1 = list.ElementAtOrDefault(6) != null ? list[6].BenefactorName : "";
            GDece1 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_1 : "";
            GDece2 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_2 : "";
            GDece3 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_3 : "";
            GSerial = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";

            HBene1 = list.ElementAtOrDefault(7) != null ? list[7].BenefactorName : "";
            HDece1 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_1 : "";
            HDece2 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_2 : "";
            HDece3 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_3 : "";
            HSerial = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";

            IBene1 = list.ElementAtOrDefault(8) != null ? list[8].BenefactorName : "";
            IDece1 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_1 : "";
            IDece2 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_2 : "";
            IDece3 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_3 : "";
            ISerial = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";

            JBene1 = list.ElementAtOrDefault(9) != null ? list[9].BenefactorName : "";
            JDece1 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_1 : "";
            JDece2 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_2 : "";
            JDece3 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_3 : "";
            JSerial = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            #endregion

            #region 2
            A2Bene1 = list.ElementAtOrDefault(10) != null ? list[10].BenefactorName : "";
            A2Dece1 = list.ElementAtOrDefault(10) != null ? list[10].DeceasedName_1 : "";
            A2Dece2 = list.ElementAtOrDefault(10) != null ? list[10].DeceasedName_2 : "";
            A2Dece3 = list.ElementAtOrDefault(10) != null ? list[10].DeceasedName_3 : "";
            A2Serial = list.ElementAtOrDefault(10) != null ? list[10].SerialCode : "";
             
            B2Bene1 = list.ElementAtOrDefault(11) != null ? list[11].BenefactorName : "";
            B2Dece1 = list.ElementAtOrDefault(11) != null ? list[11].DeceasedName_1 : "";
            B2Dece2 = list.ElementAtOrDefault(11) != null ? list[11].DeceasedName_2 : "";
            B2Dece3 = list.ElementAtOrDefault(11) != null ? list[11].DeceasedName_3 : "";
            B2Serial = list.ElementAtOrDefault(11) != null ? list[11].SerialCode : "";
             
            C2Bene1 = list.ElementAtOrDefault(12) != null ? list[12].BenefactorName : "";
            C2Dece1 = list.ElementAtOrDefault(12) != null ? list[12].DeceasedName_1 : "";
            C2Dece2 = list.ElementAtOrDefault(12) != null ? list[12].DeceasedName_2 : "";
            C2Dece3 = list.ElementAtOrDefault(12) != null ? list[12].DeceasedName_3 : "";
            C2Serial = list.ElementAtOrDefault(12) != null ? list[12].SerialCode : "";
             
            D2Bene1 = list.ElementAtOrDefault(13) != null ? list[13].BenefactorName : "";
            D2Dece1 = list.ElementAtOrDefault(13) != null ? list[13].DeceasedName_1 : "";
            D2Dece2 = list.ElementAtOrDefault(13) != null ? list[13].DeceasedName_2 : "";
            D2Dece3 = list.ElementAtOrDefault(13) != null ? list[13].DeceasedName_3 : "";
            D2Serial = list.ElementAtOrDefault(13) != null ? list[13].SerialCode : "";
             
            E2Bene1 = list.ElementAtOrDefault(14) != null ? list[14].BenefactorName : "";
            E2Dece1 = list.ElementAtOrDefault(14) != null ? list[14].DeceasedName_1 : "";
            E2Dece2 = list.ElementAtOrDefault(14) != null ? list[14].DeceasedName_2 : "";
            E2Dece3 = list.ElementAtOrDefault(14) != null ? list[14].DeceasedName_3 : "";
            E2Serial = list.ElementAtOrDefault(14) != null ? list[14].SerialCode : "";
             
            F2Bene1 = list.ElementAtOrDefault(15) != null ? list[15].BenefactorName : "";
            F2Dece1 = list.ElementAtOrDefault(15) != null ? list[15].DeceasedName_1 : "";
            F2Dece2 = list.ElementAtOrDefault(15) != null ? list[15].DeceasedName_2 : "";
            F2Dece3 = list.ElementAtOrDefault(15) != null ? list[15].DeceasedName_3 : "";
            F2Serial = list.ElementAtOrDefault(15) != null ? list[15].SerialCode : "";
             
            G2Bene1 = list.ElementAtOrDefault(16) != null ? list[16].BenefactorName : "";
            G2Dece1 = list.ElementAtOrDefault(16) != null ? list[16].DeceasedName_1 : "";
            G2Dece2 = list.ElementAtOrDefault(16) != null ? list[16].DeceasedName_2 : "";
            G2Dece3 = list.ElementAtOrDefault(16) != null ? list[16].DeceasedName_3 : "";
            G2Serial = list.ElementAtOrDefault(16) != null ? list[16].SerialCode : "";
             
            H2Bene1 = list.ElementAtOrDefault(17) != null ? list[17].BenefactorName : "";
            H2Dece1 = list.ElementAtOrDefault(17) != null ? list[17].DeceasedName_1 : "";
            H2Dece2 = list.ElementAtOrDefault(17) != null ? list[17].DeceasedName_2 : "";
            H2Dece3 = list.ElementAtOrDefault(17) != null ? list[17].DeceasedName_3 : "";
            H2Serial = list.ElementAtOrDefault(17) != null ? list[17].SerialCode : "";
             
            I2Bene1 = list.ElementAtOrDefault(18) != null ? list[18].BenefactorName : "";
            I2Dece1 = list.ElementAtOrDefault(18) != null ? list[18].DeceasedName_1 : "";
            I2Dece2 = list.ElementAtOrDefault(18) != null ? list[18].DeceasedName_2 : "";
            I2Dece3 = list.ElementAtOrDefault(18) != null ? list[18].DeceasedName_3 : "";
            I2Serial = list.ElementAtOrDefault(18) != null ? list[18].SerialCode : "";
             
            J2Bene1 = list.ElementAtOrDefault(19) != null ? list[19].BenefactorName : "";
            J2Dece1 = list.ElementAtOrDefault(19) != null ? list[19].DeceasedName_1 : "";
            J2Dece2 = list.ElementAtOrDefault(19) != null ? list[19].DeceasedName_2 : "";
            J2Dece3 = list.ElementAtOrDefault(19) != null ? list[19].DeceasedName_3 : "";
            J2Serial = list.ElementAtOrDefault(19) != null ? list[19].SerialCode : "";
            #endregion
        }
    }
}
