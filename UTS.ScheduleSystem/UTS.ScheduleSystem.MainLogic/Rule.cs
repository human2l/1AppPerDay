using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public enum Status { Approved, Rejected, Pending }
    public abstract class Rule
    {
        protected string input;
        protected string output;
        protected string relatedUsersId;
        protected Status status;
        protected string id;
        protected string lastRelatedUserID;

        public Rule(string id,  string input, string output, string relatedUsersId, Status status)
        {
            this.Id = id;
            this.input = input;
            this.output = output;
            this.relatedUsersId = relatedUsersId;
            this.status = status;
            this.lastRelatedUserID = relatedUsersId;
        }

        private string GetLastRelatedUserID()
        {
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastUserId = relatedUsersIdString[relatedUsersIdString.Length - 1];
            return lastUserId;
        }


        public string Input
        {
            get
            {
                return input;
            }

            set
            {
                input = value;
            }
        }

        public string Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
            }
        }

        public string RelatedUsersId
        {
            get
            {
                return relatedUsersId;
            }

            set
            {
                relatedUsersId = value;
            }
        }

        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
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

        public string LastRelatedUserID
        {
            get
            {
                return GetLastRelatedUserID();
            }

            set
            {
                lastRelatedUserID = value;
            }
        }
    }
}
