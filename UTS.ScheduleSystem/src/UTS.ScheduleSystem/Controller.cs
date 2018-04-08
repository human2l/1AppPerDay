﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class Controller
    {
        private FakeDB fakeDB = new FakeDB();

        public FakeDB FakeDB
        {
            get
            {
                return fakeDB;
            }

            set
            {
                fakeDB = value;
            }
        }

        public void initialization()
        {
            User frank = new User("u001", "Frank", "frank", "frank@frank.com", Role.DMnA);
            ConversationalRule weatherRule1 = new ConversationalRule("c001", "Weather", "How is the weather on ", "The weather on {p1} is {p2}", "u001 u002", Status.Pending);
            FixedConversationalRule weatherFRule1 = new FixedConversationalRule("fc002", "Greeting", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            MealSchedule mealSchedule1 = new MealSchedule("m001", "dinner", "Michael Bay,Donald Trump", "Sydney", "08/04/2018 3:12:18 PM", "08/04/2018 4:15:00 PM");
            fakeDB.UserTbl.Add(frank);
            fakeDB.CRulesTbl.Add(weatherRule1);
            fakeDB.FCRulesTbl.Add(weatherFRule1);
            fakeDB.MealScheduleTbl.Add(mealSchedule1);

        }
        //dummy code for test







    }
}
