using Red_Folder.com.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public class ActivityRepository : IActivityRepository
    {
        private ActivityClient _client;

        public ActivityRepository(string activityUrl, string activityCode)
        {
            _client = new ActivityClient(activityUrl, activityCode);
        }

        public Weekly Weekly(int year, int weekNumber)
        {
            return _client.Weekly(year, weekNumber);
        }
    }
}
