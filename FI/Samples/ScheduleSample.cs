using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.ScheduleDataServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Schedule data integration service supports following operations: 
    ///  * Fetching schedule data
    ///  * Fetching planned homecare visits
    ///  * Fetching queue data
    ///  * Appointment status updates
    ///  * Fecthing free appointment slots
    ///  * Create an appointment
    ///  * Cancel an appointment
    ///  * Reschedule an appointment
    /// </summary>
    class ScheduleSample : SampleBase
    {
        public static async Task<int> CallScheduleService()
        {
            var service = new ScheduleDataServiceClient();

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

            var common = new ScheduleDataServiceReference.CommonRequestData()
            {
                ContractKey = ContractKey,
                CallingSystem = CallingSystem,
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "kkh" },
                UserIdentifiers = new UserIdentifier[] { new UserIdentifier() { UserIdentifierCodeId = UserIdentifierCodeId.EFFICA_USER_ID, UserIdentifierCode = UserId } },
                Function = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "kotih" },
                ReasonCode = new Code() { CodeSetName = "THL- Potilastietojen katselun erityinen syy 2012", CodeValue = "2" } // Coding system: http://91.202.112.142/codeserver/pages/code-list-page.xhtml?returnLink=1
            };

            // Initialize Request
            var req = new ScheduleReq() 
            {
                ScheduleId = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = UserId },
                PatientId = new CustomerId() { Identifier="010101-0101" },
                StartDateTime = DateTime.Now.AddDays(-1.0),
                EndDateTime = DateTime.Now,
                StatusCode = StatusCode.Unknown,
                RequestType = RequestType.All
            };

            // Structure for return data
            var rsp = new Schedule[100];

            try
            {
                service.GetScheduleCommonData(ref header, common, req, out rsp);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
