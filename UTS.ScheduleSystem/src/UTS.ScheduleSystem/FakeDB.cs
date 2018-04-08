﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
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

        //public FakeDB(LinkedList<User> userTbl, LinkedList<ConversationalRule> cRulesTbl, LinkedList<FixedConversationalRule> fCRulesTbl, LinkedList<MealSchedule> mealScheduleTbl)
        //{
        //    this.userTbl = userTbl;
        //    this.cRulesTbl = cRulesTbl;
        //    this.fCRulesTbl = fCRulesTbl;
        //    this.mealScheduleTbl = mealScheduleTbl;
        //}


    }
}