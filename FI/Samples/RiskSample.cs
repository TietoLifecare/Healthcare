using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifecareAPI.RiskDataServiceReference;

namespace LifecareAPI.Samples
{
    /// <summary>
    /// Risk data integration service - via this service the external system can request risk data from Lifecare’s risk register.
    /// </summary>
    class RiskSample : SampleBase
    {
        public static async Task<int> CallRiskService()
        {
            var service = new RiskDataIntegrationServiceClient();

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
            var req = new Patient() { PatientId = new PatientId() { Identifier = "010101-0101" } };

            // Structure for return data
            var rsp = new RiskData();

            service.GetRiskData(ref header, common, req, out rsp);

            return 0;
        }
    }
}
