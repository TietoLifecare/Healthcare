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
                Originator = "Hackathon",
                Created = DateTime.Now.AddDays(-1.0),
                Finished = DateTime.Now,
                FormsetCode = "E121",
                FormsetCodesystem = "1.123.234.567",
                FormsetName = "Testaajan testikysely",
                Organisation = "Kalvolan TK",
                OrganisationOid = "1.2.246.10.19623654.19.1",
                OrgUnit = "Kehvon Anamneesitutkimuskeskus",
                OrgUnitOid = "121.121.121.234.0",
                Severity=1,            
                Status = Status.FORMSET_RECEIVED // Status - enumeration: [FORMSET_SENT,FORMSET_RECEIVED,DATA_AS_IMAGE]            
            };

            var contactMeta = new ContactMetadata[] 
            {
                new ContactMetadata() { Key="AR12312", Value="MetaValue1" }
            };

            var forms = new Form[] 
            {
                new Form() 
                { 
                    FormCode = "A3399T",
                    FormCodeSystem = "123.123.123.444",
                    FormName = "Testform",
                    Order=1,
                    Severity=1,
                    Questions = new Question[] 
                    {
                        new Question() 
                        { 
                            QuestionCode="1", 
                            QuestionCodeSystem="123.456.789.10", 
                            QuestionText="Kysymys yksi", 
                            Order=1, 
                            Language="fi", 
                            Answer = new Answer() 
                            { 
                                AnswerCode=1, 
                                AnswerCodeSystem="123.456.123.10", 
                                AnswerText="Vastaus kysymykseen yksi" 
                            } 
                        },
                        new Question() 
                        { 
                            QuestionCode="2", 
                            QuestionCodeSystem="123.456.789.10", 
                            QuestionText="Kysymys kaksi", 
                            Order=1, 
                            Language="fi", 
                            Severity=1,
                            ParentQuestion="1",
                            Answer = new Answer() 
                            { 
                                AnswerCode=2, 
                                AnswerCodeSystem="123.123.123", 
                                AnswerText="Vastaus kysymykseen kaksi joka liittyy parentquestion-arvon kautta kysymykseen yksi" 
                            } 
                        },
                        new Question() 
                        { 
                            QuestionCode="3", 
                            QuestionCodeSystem="123.456.789.10", 
                            QuestionText="Kysymys kolme, jolle ei ole annettu vastausta. Answer osuus puuttuu.", 
                            Order=3, 
                            Language="fi", 
                            Severity=2
                        }
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
