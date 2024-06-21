using Elsa.Services.Messaging;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    public class PrintPlaquePost
    {
        /// <summary>
        /// Guid key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 按鈕顯示文本
        /// </summary>
        public string ButtonDisplayName { get; set; }

        /// <summary>
        /// 模版文件的路徑
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 每頁蓮位數
        /// </summary>
        public int SeatCount { get; set; }

        public FileTypeEnum FileType { get; set; }

        public PlaqueTypeEnum PlaqueType { get; set; }
    }

    public enum FileTypeEnum
    {
        Excel = 10,
        Word
    }

    public enum PlaqueTypeEnum
    {
        延生 = 10,
        附薦
    }
}
