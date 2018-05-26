using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.Data
{
    public class Rule
    {
        private string input;
        private string output;
        private string relatedUsersId;
        private string status;

        public Rule()
        {

        }

        public Rule(string input, string output, string relatedUsersId, string status)
        {
            this.input = input;
            this.output = output;
            this.relatedUsersId = relatedUsersId;
            this.status = status;

        }
        //public int Id { get; set; }
        public string Input { get => input; set => input = value; }
        public string Output { get => output; set => output = value; }
        public string RelatedUsersId { get => relatedUsersId; set => relatedUsersId = value; }
        public string Status { get => status; set => status = value; }
    }
}
