using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WpfEPRTester.AnamnesisDataService;

namespace WpfEPRTester.Samples
{
    /// <summary>
    /// Anamnesis data integration service
    /// </summary>
    class AnamnesisSample : SampleBase
    {
        public static async Task<int> CallAnamnesisService()
        {
            var service = new EsitiedotClient();

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
            var contact = new Contact()
            {
                // 010100-A011 -- RID 1, ServiceEvent 1.2.246.10.19623654.10.1.14010.2013.947
                // 010101-0101 -- ServiceEvent 1.2.246.10.19623654.10.3.14669.2015.87
                PatientId = new PatientId() { Identifier = "010101-0101" },
                Organisation = "317",
                FormsetCode = "OTK01",
                FormsetCodesystem = "1.2.246.537.10.999",
                OrganisationOid = "1.2.246.537.10.55",
                OrgUnit = "kkh",
                OrgUnitOid = "1.2.246.537.10.55.3",
                Severity = 1,
                Status = Status.FORMSET_SENT,
                Originator = "Hackathon",
                Created = DateTime.Now,
                Finished = DateTime.Now,
                FormsetName = "Hackathon Questionaire"                 
            };

            var contactMeta = new ContactMetadata[] 
            {
                new ContactMetadata() { Key="MetaKey1", Value="MetaValue1" }
            };

            var forms = new Form[] 
            {
                new Form() 
                { 
                    FormCode = "1",
                    FormCodeSystem = "1.2.246.537.10.999",
                    FormName = "Hackathon",
                    Order=1,
                    Severity=1,
                    Questions = new Question[] 
                    {
                        new Question() { QuestionCode="1", QuestionCodeSystem="1.2.246.537.10.999", QuestionText="Hack?", Order=1, Language="en", Answer = new Answer() { AnswerCode=1, AnswerCodeSystem="1.2.246.537.1", AnswerText="Who knows?" } }
                    }
                }
            };

            // Structure for return data
            var patient = new PatientId();
            
            try
            {
                service.AddEsitieto(ref header, common, contact, contactMeta, forms, out patient);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
