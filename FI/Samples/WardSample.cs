using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.WardStatusServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Ward data integration service 
    /// </summary>
    class WardSample : SampleBase
    {
        public static async Task<int> CallWardService()
        {
            var service = new WardStatusServiceClient();

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
            var req = new WardReq() 
            {
                Collection = "M", // M - Changes after given Timestamp,  K - Everything
                Timestamp = DateTime.Now.AddDays(-30.0),
                Hospital = "317", // Hospital ID
                Ward = "1111T" // Ward ID                 
            };

            // Structure for return data
            var rsp = new WardStatus();

            try
            {
                service.GetPaikkatilanne(ref header, common, req, out rsp);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
