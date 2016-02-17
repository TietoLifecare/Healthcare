using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.MedicationServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Medication data integration service - via this service the external system can request open care medication (home medication) entered in Lifecare.
    /// </summary>
    class MedicationSample : SampleBase
    {
        public static async Task<int> CallMedicationService()
        {
            var service = new MedicationServiceClient();

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

            var medicationReq = new MedicationReq() 
            {
                Area = new Code() { CodeSetName="Effica/Lifecare", CodeValue="pth" },
                Organisation = new Code{ CodeSetName="Effica/Lifecare", CodeValue="317" },
                PatientId = new PatientId() { Identifier="010101-0101" }
            };

            // Structures for return data
            var patientMedication = new PatientMedication[1];

            try
            {
                service.GetOpenCareMedicationData(ref header, common, medicationReq, out patientMedication);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
