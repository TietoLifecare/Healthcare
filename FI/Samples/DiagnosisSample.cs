using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.DiagnosisServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Risk data integration service - via this service the external system can request risk data from Lifecare’s risk register.
    /// </summary>
    class DiagnosisSample : SampleBase
    {
        public static async Task<int> CallDiagnosisService()
        {
            var service = new DiagnosisServiceClient();

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
            var req = new DiagnosisReq()
            {
                Organisation = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "317" },
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "kotih" },
                EffectiveTime = new EffectiveTime() { StartDateTime = DateTime.Now.AddDays(-365.0), EndDateTime = DateTime.Now },
                PatientId = new PatientId() { Identifier = "121221-A020" }
            };

            // Structure for return data     
            var patientId = new PatientId();
            var diagnoses = new Diagnose[100];
            
            try
            {
                service.GetPatientDiagnoses(ref header, common, req, out patientId, out diagnoses);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
