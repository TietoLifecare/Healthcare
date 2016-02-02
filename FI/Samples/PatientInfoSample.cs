using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifecareAPI.PatientDataServiceReference;

namespace LifecareAPI.Samples
{
    /// <summary>
    /// Patient demographic data integration service supports following operations:
    ///  * requests from Effica patient data like customer data, next of kin, etc.
    ///  * update parts of patient data in Effica, e.g. other address, phone numbers, e-mail address and occupation.
    ///  * maintain patient’s next of kin data.
    /// </summary>
    class PatientInfoSample : SampleBase
    {
        public static async Task<int> CallPatientInfoService()
        {
            var service = new PatientDataIntegrationServiceClient();

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

            var patient = new PatientId()
            {
                Identifier = "010101-0101"
            };

            PatientDataServiceReference.Patient[] patients = new Patient[1];

            service.GetPatientData(ref header, common, patient, out patients);

            return 0;
        }
    }
}
