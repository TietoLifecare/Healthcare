using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.EPRDocUploadServiceReference;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Patient record integration service - via this service the external system can transfer patient records into Lifecare.
    /// Sample for interface versions 3.0 / 10.0
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

            var common = new CommonRequestData()
            {
                ContractKey = ContractKey,
                CallingSystem = CallingSystem,
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "2103Y" },
                UserIdentifiers = new UserIdentifier[] { new UserIdentifier() { UserIdentifierCodeId = UserIdentifierCodeId.EFFICA_USER_ID, UserIdentifierCode = UserId } },
                Function = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "esh" },
                ReasonCode = new Code() { CodeSetName = "THL- Potilastietojen katselun erityinen syy 2012", CodeValue = "2" } // Coding system: http://91.202.112.142/codeserver/pages/code-list-page.xhtml?returnLink=1
            };

            var contact = new Contact()
            {
                // 010100-A011 -- RID 1, ServiceEvent 1.2.246.10.19623654.10.1.14010.2013.947
                // 010101-0101 -- ServiceEvent 1.2.246.10.19623654.10.3.14669.2015.87
                // 030303-0303 -- 1.2.246.10.19623654.10.1.14013.2015.1447
                PatientId = new PatientId() { Identifier = "030303-0303" }, 
                VisitTime = DateTime.Now,
                ContactAuthor = new UserIdentifier[] { new UserIdentifier() { UserIdentifierCodeId = UserIdentifierCodeId.EFFICA_USER_ID, UserIdentifierCode = UserId } },
                Organisation = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "317" },
                Form = "YLE",
                Speciality = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "10" },
                DataState = DataState.A,
                CareProcesState = CareprocessState.TULOTILANNE,
                ContactRows = new ContactRow[] { 
                        new EPRDocUploadServiceReference.ContactRow() { 
                            HeadingName = "Esitiedot", 
                            Data = "Data",
                            Value = "Value",
                            Specifier = "Specifier II"
                        }
                    },
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "2103Y" },
                Function = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "esh" },
                ServiceEvent = new Code() { CodeSetName = "OID", CodeValue = "1.2.246.10.19623654.10.1.14013.2015.1447" } // Should be queried via service.GetServiceEventData                 
            };

            var serviceEventReq = new ServiceEventReq() 
            {
                 Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "2103Y" },
                 Function = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "esh" },
                 Organisation = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "317" },
                 RegisterType = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "2" },
                 PatientId = new PatientId() { Identifier = "030303-0303" }                  
            };

            // Structures for return data
            var patient = new EPRDocUploadServiceReference.PatientId();
            var guid = new Guid();
            int row;
            
            var serviceEventRsp = new ServiceEventResp[100];
            

            try
            {
                service.GetServiceEventData(ref header, common, serviceEventReq, out serviceEventRsp);
                service.InsertEPRData(ref header, common, contact, out patient, out guid, out row);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
