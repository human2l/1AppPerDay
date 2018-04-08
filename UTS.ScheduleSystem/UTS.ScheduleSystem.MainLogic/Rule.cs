using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public enum Status { Approved, Rejected, Pending }
    public abstract class Rule
    {
        private string name;
        private string input;
        private string output;
        private string relatedUsersId;
        private Status status;
        private string id;

        public Rule(string id, string name, string input, string output, string relatedUsersId, Status status)
        {
            this.Id = id;
            this.name = name;
            this.input = input;
            this.output = output;
            this.relatedUsersId = relatedUsersId;
            this.status = status;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
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
        
    }
}
