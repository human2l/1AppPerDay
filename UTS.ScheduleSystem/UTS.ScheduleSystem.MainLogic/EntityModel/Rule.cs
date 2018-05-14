using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace UTS.ScheduleSystem.MainLogic
{
    //public enum Status { Approved, Rejected, Pending }
    public abstract class Rule
    {
        protected string input;
        protected string output;
        protected string relatedUsersId;
        protected string status;
        protected string id;
        protected string lastRelatedUserID;

        public Rule()
        {

        }
        public Rule(string id,  string input, string output, string relatedUsersId, string status)
        {
            this.id = id;
            this.input = input;
            this.output = output;
            this.relatedUsersId = relatedUsersId;
            this.status = status;
        }

        private string GetLastRelatedUserID()
        {
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastID = relatedUsersIdString[relatedUsersIdString.Length - 1];
            return lastID;
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

        public string Status
        {
            get
            {
                return status;
                //return Utils.GetStatus(status);
            }

            set
            {
                status = value;
                //status = value.ToString();
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

            //set
            //{
            //    lastRelatedUserID = value;
            //}
        }
    }
}
