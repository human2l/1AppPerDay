using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class FixedConversationalRule : Rule
    {
        public FixedConversationalRule(string id, string input, string output, string relatedUsersId, Status status) : base(id, input, output, relatedUsersId, status)
        {
        }
    }
}
