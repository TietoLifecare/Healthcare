using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LifecareAPI.StatisticsServiceReference;

namespace LifecareAPI.Samples
{
    /// <summary>
    /// Statistics data integration service - via this service the external system submit patient’s statistical events (visits) to Lifecare.
    /// All back office processes are based on the statistics data
    /// </summary>
    class StatisticsSample : SampleBase
    {
        public static async Task<int> CallStatisticsService()
        {
            var service = new StatisticsClient();

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
            var req = new CommonRequestData() 
            {
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "Kkesk" }, // Department ID
                ContactAuthor = new ContactAuthor() { ContactAuthorCodeId=CaCodeId.EfficaUserId, ContactAuthorCode=UserId }
                // FunctionType = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "08" } // Optional
            };

            var commonStats = new CommonStatisticsData()
            {
                /*
                EventType = new EventType() 
                { 
                    Event="ADD", // ADD/MOD/DEL  
                    PatientId = new PatientId() { Identifier = "010101-0101" }, 
                    StartDateTime = DateTime.Now, 
                    UserId = UserId 
                } 
                */
            };

            var StatsData = new StatisticsData() 
            {
                PatientId = new PatientId() { Identifier = "010101-0101" }, 
                UserId = UserId,
                Area = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "Kkesk" },
                RequestArea = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "Kkesk" },
                Function = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "kotih" },
                ContactType = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "1" }, // 1 = doctor appointment, 2 = home visit
                VisitType = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "2" }, // Visit type data as numeral value, E.g 1 = first time visit 2= revisit 
                ContentNotes = new ContentNote[] { new ContentNote() { Content="INFL1", Amount=1 } }, // Content note for Influenza flue shot
                ProcedureClasses = new Code[] { new Code() { CodeSetName = "", CodeValue = "SPAT1009" } }, // SPAT codes, see coding in "koodistopalvelu", http://koodistopalvelu.kanta.fi/codeserver/pages/classification-view-page.xhtml?classificationKey=310&versionKey=387
                StartDateTime = DateTime.Now.AddDays(-1.0), 
                EndDateTime = DateTime.Now,
                VisitUrgency = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "E" }, // E - No, K - Yes
                IsFirstVisit = new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "E" },  // E - No, K - Yes
                FollowUpCares = new Code[] { new Code() { CodeSetName = "", CodeValue = "SPAT1334" } }, // SPAT codes (SPAT1334 no follow-up actions)
                VisitReasons = new Code[] { new Code() { CodeSetName = "Effica/Lifecare", CodeValue = "A25" } } // ICPC2 code
            };

            var GenericStats = new GenericStatData() {}; // Not in use

            // Structure for return data
            var rsp = new StatisticsOperation();

            try
            {
                service.AddStatistics(ref header, common, req, commonStats, StatsData, GenericStats, out rsp);             
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            return 0;
        }
    }
}
