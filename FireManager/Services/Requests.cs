using System;
using System.Collections.Generic;
using FireManager.Extensions;
using FireManager.Queries;
using Microsoft.Extensions.Options;

namespace FireManager.Services
{
    public class Requests : IRequests
    {
        private readonly FireManagerOptions Options;
        
        public Requests(IOptions<FireManagerOptions> Options)
        {
            this.Options = Options.Value;
        }

        private string AccountId => Options.Accid;
        private string AccountKey => Options.AccKey;        

        public IDictionary<string, string> AllSchedulesByMonth(DateTime RequestDate)
        {
            IDictionary<string, string> values = new Dictionary<string, string>()
            {
                ["accid"] = AccountId,
                ["acckey"] = AccountKey,
                ["cmd"] = "getScheduledTimeRanges",
                ["bt"] = RequestDate.ToUniversalTime().ToString("s") + "Z",
                ["et"] = RequestDate.AddMonths(1).ToUniversalTime().ToString("s") + "Z",
                ["sch"] = "all",
                ["isp"] = "1",
                ["itt"] = "0"
            };
            return values;
        }
        public IDictionary<string, string> AllSchedulesByDate(DateTime RequestDate)
        {
            DateTime Request = new DateTime(RequestDate.Year, RequestDate.Month, RequestDate.Day, 5, 0, 0);
            IDictionary<string, string> values = new Dictionary<string, string>()
            {
                ["accid"] = AccountId,
                ["acckey"] = AccountKey,
                ["cmd"] = "getScheduledTimeRanges",
                ["bt"] = Request.ToUniversalTime().ToString("s") + "Z",
                ["et"] = Request.AddHours(24).ToUniversalTime().ToString("s") + "Z",
                ["sch"] = "all",
                ["isp"] = "1",
                ["itt"] = "0"
            };
            return values;
        }
        public IDictionary<string, string> AllSchedulesByDateRange(DateTime StartDate, DateTime EndDate)
        {
            return new Dictionary<string, string>()
            {
                ["accid"] = AccountId,
                ["acckey"] = AccountKey,
                ["cmd"] = "getScheduledTimeRanges",
                ["bt"] = StartDate.ToUniversalTime().ToString("s") + "Z",
                ["et"] = EndDate.ToUniversalTime().ToString("s") + "Z",
                ["sch"] = "all",
                ["isp"] = "1",
                ["itt"] = "0"
            };
        }
        public IDictionary<string, string> AllSchedulesRequest => new Dictionary<string, string>()
        {
            ["accid"] = AccountId,
            ["acckey"] = AccountKey,
            ["cmd"] = "getSchedules",
            ["isp"] = "1"
        };
        public IDictionary<string, string> AllMembersRequest => new Dictionary<string, string>()
        {
            ["accid"] = AccountId,
            ["acckey"] = AccountKey,
            ["cmd"] = "getMembers",
            ["ia"] = "all",
            ["only_active"] = "0"
        };
        public IDictionary<string, string> AllActiveMembersRequest => 
            new Dictionary<string, string>()
        {
            ["accid"] = AccountId,
            ["acckey"] = AccountKey,
            ["cmd"] = "getMembers",
            ["ia"] = "all",
            ["only_active"] = "1"
        };
    }
}