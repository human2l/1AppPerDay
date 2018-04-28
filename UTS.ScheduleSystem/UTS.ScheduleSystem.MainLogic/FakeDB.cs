using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class FakeDB
    {
        private List<User> userTbl = new List<User>();
        private List<ConversationalRule> cRulesTbl = new List<ConversationalRule>();
        private List<FixedConversationalRule> fCRulesTbl = new List<FixedConversationalRule>();
        private List<MealSchedule> mealScheduleTbl = new List<MealSchedule>();

        public List<User> UserTbl
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

        public List<ConversationalRule> CRulesTbl
        {
            get
            {
                return cRulesTbl;
            }

            set
            {
                cRulesTbl = value;
            }
        }

        internal List<FixedConversationalRule> FCRulesTbl
        {
            get
            {
                return fCRulesTbl;
            }

            set
            {
                fCRulesTbl = value;
            }
        }

        public List<MealSchedule> MealScheduleTbl
        {
            get
            {
                return mealScheduleTbl;
            }

            set
            {
                mealScheduleTbl = value;
            }
        }



    }
}
