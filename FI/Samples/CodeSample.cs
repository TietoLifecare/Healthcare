using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEPRTester.CodeServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Code sets integration service - via this service the external system can request Lifecare’s internal code sets.
    ///  * Homecare codes
    /// </summary>
    class CodeSample : SampleBase
    {
        public static async Task<int> CallCodeService()
        {
            var service = new CodeServiceClient();

            var header = new MessageHeader()
            {
                MessageID = "101",
                TransactionID = "101",
                SenderID = "101",
                SenderApplication = "Debug",
                ReceiverID = "101",
                ReceiverApplication = "Effica",
                CharacterSet = "UTF-16"
            };

            var common = new LisCommon()
            {
                ContractKey = ContractKey,
                UserId = UserId,
                CallingSystem = CallingSystem,
                CallingUserId = UserId
            };

            // Initialize Request
            var req = new CodeReq() { CodeSetId = CodeSet.KHPalv };

            // Structure for return data
            var rsp = new IntegrationCodeService();

            service.GetCodeService(ref header, common, req, out rsp);

            return 0;
        }
    }
}
