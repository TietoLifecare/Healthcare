using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifecareAPI.EPRDocUploadServiceReference;

namespace LifecareAPI.Samples
{
    /// <summary>
    /// Patient record integration service - via this service the external system can transfer patient records into Lifecare.
    /// </summary>
    class EPRSample : SampleBase
    {
        public static async Task<int> CallEPRDocUploadService()
        {
            var service = new EPRDocUploadServiceClient();

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

            var contact = new Contact()
            {
                PatientId = new PatientId() { Identifier = "260116A901S" }, // 010101-0101
                VisitTime = DateTime.Now,
                ContactAuthor = "tieto",
                Organisation = "317",
                OrgUnit = "kkh",
                Form = "YLE",
                Functionary = "kotih",
                Speciality = "10",
                DataState = DataState.A,
                CareprocesState = CareprocessState.TULOTILANNE,
                ContactRows = new ContactRow[] { 
                        new EPRDocUploadServiceReference.ContactRow() { 
                            HeadingName = "Esitiedot", 
                            Data = "Data",
                            Value = "Value",
                            Specifier = "Specifier II"
                        }
                    }
            };

            var patient = new EPRDocUploadServiceReference.PatientId();

            service.InsertEPRData(ref header, common, contact, out patient);

            return 0;
        }
    }
}
