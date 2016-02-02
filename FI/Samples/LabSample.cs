using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifecareAPI.LaboratoryServiceReference;

namespace LifecareAPI.Samples
{
    /// <summary>
    /// Laboratory data integration service - via this service the external system can request laboratory results entered in Lifecare.
    /// </summary>
    class LabSample : SampleBase
    {
        public static async Task<int> CallLabService()
        {
            var service = new LaboratoryServiceClient();

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
            var req = new LaboratoryRequest()
            {
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "pth" },
                Organisation = new Code { CodeSetName = "Effica/Lifecare", CodeValue = "317" },
                PatientId = new PatientId() { Identifier = "010101-0101" },
                EffectiveTime = new EffectiveTime() { StartDateTime = DateTime.Now.AddDays(-365.0), EndDateTime = DateTime.Now }
            };

            // Structure for return data
            var rsp1 = new PatientId();
            var rsp2 = new LaboratoryResult[100];

            service.GetPatientLaboratoryResults(ref header, common, req, out rsp1, out rsp2);
            
            return 0;
        }
    }
}
