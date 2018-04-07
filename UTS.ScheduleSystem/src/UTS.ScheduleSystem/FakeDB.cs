using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class FakeDB
    {
        private LinkedList<User> userTbl;
        private LinkedList<ConversationalRule> CRulesTbl;
        private LinkedList<FixedConversationalRule> FCRulesTbl;
        private LinkedList<MealSchedule> mealScheduleTble;

        public LinkedList<User> UserTbl
        {
            get
            {
                return userTbl;
            }

            set
            {
                userTbl = value;
            }
        }

        public LinkedList<ConversationalRule> CRulesTbl1
        {
            get
            {
                return CRulesTbl;
            }

            set
            {
                CRulesTbl = value;
            }
        }

        internal LinkedList<FixedConversationalRule> FCRulesTbl1
        {
            get
            {
                return FCRulesTbl;
            }

            set
            {
                FCRulesTbl = value;
            }
        }

        public LinkedList<MealSchedule> MealScheduleTble
        {
            get
            {
                return mealScheduleTble;
            }

            set
            {
                mealScheduleTble = value;
            }
        }
    }
}
