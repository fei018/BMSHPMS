using Aliyun.OSS.Model;
using NPOI.SS.Formula.Functions;

namespace BMSHPMS.Helper
{
    public class VMResult
    {
        public string Message { get; set; }

        public bool Succed { get; set; }

        public VMResult Error(string msg = null)
        {
            return new VMResult { Message = msg, Succed = false };
        }

        public VMResult Success(string msg = null)
        {
            return new VMResult { Succed = true, Message = msg };
        }
    }
}
