using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class FixedConversationalRule : Rule
    {
        public FixedConversationalRule(string id, string name, string input, string output, string relatedUsersId, Status status) : base(id, name, input, output, relatedUsersId, status)
        {
        }
    }
}
