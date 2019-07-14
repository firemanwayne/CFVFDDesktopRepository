using System;
using System.Collections.Generic;

namespace FireManager.Queries
{
    public interface IRequests
    {        
        IDictionary<string, string> AllMembersRequest { get; }
        IDictionary<string, string> AllActiveMembersRequest { get; }
        IDictionary<string, string> AllSchedulesRequest { get; }

        IDictionary<string, string> AllSchedulesByMonth(DateTime RequestDate);
        IDictionary<string, string> AllSchedulesByDate(DateTime RequestDate);
        IDictionary<string, string> AllSchedulesByDateRange(DateTime StartDate, DateTime EndDate);
    }
}
