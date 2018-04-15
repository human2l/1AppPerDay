using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class MealSchedule
    {
        private string id;
        private string userId;
        private string topic;
        private string participants;
        private string location;
        private string startDate;
        private string endDate;
        private string lastEditUserId;

        public MealSchedule(string id, string userId, string topic, string participants, string location, string startDate, string endDate, string lastEditUserId)
        {
            this.id = id;
            this.userId = userId;
            this.topic = topic;
            this.participants = participants;
            this.location = location;
            this.startDate = startDate;
            this.endDate = endDate;
            this.lastEditUserId = lastEditUserId;
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Topic
        {
            get
            {
                return topic;
            }

            set
            {
                topic = value;
            }
        }

        public string Participants
        {
            get
            {
                return participants;
            }

            set
            {
                participants = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public string StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public string LastEditUserId
        {
            get
            {
                return lastEditUserId;
            }

            set
            {
                lastEditUserId = value;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
    }
}
