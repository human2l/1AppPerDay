using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem
{
    public class Controller
    {
        private FakeDB fakeDB = new FakeDB();
        private User currentUser;
        private List<User> userList = new List<User>();
        private List<ConversationalRule> conversationalRulesList = new List<ConversationalRule>();
        private List<FixedConversationalRule> fixedConversationalRulesList = new List<FixedConversationalRule>();
        private List<MealSchedule> mealScheduleList = new List<MealSchedule>();
        //private List<Rule> ruleList = new List<Rule>();

        private DataMaintainerService dataMaintainerService = new DataMaintainerService();
        private EditorService editorService = new EditorService();
        private ApproverService approverService = new ApproverService();




        public Controller()
        {
            initialization();

        }




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

        public DataMaintainerService DataMaintainerService
        {
            get
            {
                return dataMaintainerService;
            }

            set
            {
                dataMaintainerService = value;
            }
        }

        public EditorService EditorService
        {
            get
            {
                return editorService;
            }

            set
            {
                editorService = value;
            }
        }

        public ApproverService ApproverService
        {
            get
            {
                return approverService;
            }

            set
            {
                approverService = value;
            }
        }

        public List<User> UserList
        {
            get
            {
                var adapter = new AspNetUsersTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
                if (set.Count >= 1)
                {
                    User DMnEnA = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                    set.RemoveAspNetUsersRow(set.First());
                    userList.Add(DMnEnA);
                }
                if (set.Count >= 3)
                {
                    User DM = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                    set.RemoveAspNetUsersRow(set.First());
                    User E = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                    set.RemoveAspNetUsersRow(set.First());
                    User A = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                    set.RemoveAspNetUsersRow(set.First());
                    userList.Add(DM);
                    userList.Add(E);
                    userList.Add(A);
                }

                return userList;
            }

            set
            {
                userList = value;
            }
        }

        public List<ConversationalRule> ConversationalRulesList
        {
            get
            {
                var adapter = new ConversationalRuleTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
<<<<<<< HEAD
                //while (set.Count != 0)
                //{
                //    ConversationalRule cRule = new ConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                //        (set.First().status.Equals((Status.Approved).ToString())) ? Status.Approved :
                //        (set.First().status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                //        Status.Pending);
                //    set.RemoveMealScheduleRow(set.First());
                //    conversationalRulesList.Add(cRule);
                //}
=======
                while (set.Count != 0)
                {
                    ConversationalRule cRule = new ConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                        Status.Pending);
                    set.RemoveConversationalRuleRow(set.First());
                    conversationalRulesList.Add(cRule);
                }
>>>>>>> aa1f23f1ccf0d9b2f6f9d5af3bdf833d8bdce766
                return conversationalRulesList;
            }

            set
            {
                conversationalRulesList = value;
            }
        }

        public List<FixedConversationalRule> FixedConversationalRulesList
        {
            get
            {
                var adapter = new FixedConversationalRuleTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
<<<<<<< HEAD
                //while (set.Count != 0)
                //{
                //    FixedConversationalRule fcRule = new FixedConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                //        (set.First().status.Equals((Status.Approved).ToString())) ? Status.Approved :
                //        (set.First().status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                //        Status.Pending);
                //    set.RemoveMealScheduleRow(set.First());
                //    fixedConversationalRulesList.Add(fcRule);
                //}
=======
                while (set.Count != 0)
                {
                    FixedConversationalRule fcRule = new FixedConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                        Status.Pending);
                    set.RemoveFixedConversationalRuleRow(set.First());
                    fixedConversationalRulesList.Add(fcRule);
                }
>>>>>>> aa1f23f1ccf0d9b2f6f9d5af3bdf833d8bdce766
                return fixedConversationalRulesList;
            }

            set
            {
                fixedConversationalRulesList = value;
            }
        }

        public List<MealSchedule> MealScheduleList
        {
            get
            {
                var adapter = new MealScheduleTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
                while (set.Count != 0)
                {
                    MealSchedule ms = new MealSchedule(set.First().Id, set.First().UserId, set.First().Topic, set.First().Participants, set.First().Location, set.First().StartDate, set.First().EndDate, set.First().LastEditUserId);
                    set.RemoveMealScheduleRow(set.First());
                    mealScheduleList.Add(ms);
                }

                return mealScheduleList;
            }

            set
            {
                var adapter = new MealScheduleTableAdapter();
                var set = adapter.GetData();
                //while (set.Count != 0)
                //{
                //    set.RemoveMealScheduleRow(set.First());
                //}
                //while (MealScheduleList.Count != 0)
                //{
                //    MealSchedule ms = MealScheduleList.
                //}

                mealScheduleList = value;
            }
        }

        public User CurrentUser
        {
            get
            {
                return currentUser;
            }

            set
            {
                currentUser = value;
            }
        }

        public void initialization()
        {
            User frank = new User("u001", "Frank", "frank", "frank@frank.com", Role.DMnA);
            ConversationalRule weatherRule1 = new ConversationalRule("c001", "How is the weather on ", "The weather on {p1} is {p2}", "u001 u002", Status.Approved);
            FixedConversationalRule weatherFRule1 = new FixedConversationalRule("fc001", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Rejected);
            FixedConversationalRule weatherFRule2 = new FixedConversationalRule("fc002", "qwerty", "asdfgg", "u001", Status.Pending);
            MealSchedule mealSchedule1 = new MealSchedule("ms1", "u001", "dinner", "Michael Bay,Donald Trump", "Sydney", "08/04/2018 3:12:18 PM", "08/04/2018 4:15:00 PM", "u001 u002");
            MealSchedule ms1 = new MealSchedule("ms2", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
            MealSchedule ms2 = new MealSchedule("ms3", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
            //fakeDB.UserTbl.Add(frank);
            //fakeDB.CRulesTbl.Add(weatherRule1);
            //fakeDB.FCRulesTbl.Add(weatherFRule1);
            //fakeDB.MealScheduleTbl.Add(mealSchedule1);
            UserList.Add(frank);
            conversationalRulesList.Add(weatherRule1);
            fixedConversationalRulesList.Add(weatherFRule1);
            fixedConversationalRulesList.Add(weatherFRule2);
            MealScheduleList.Add(mealSchedule1);
            MealScheduleList.Add(ms1);
            MealScheduleList.Add(ms2);


            //codes for read database and update all lists


        }

        //main process
        public void maintaining()
        {

        }


        public bool handleConversation(string userInput)
        {
            return false;
        }

        private Role GetRole (string role)
        {
            switch (role)
            {
                case "DMnEnA":
                    return Role.DMnEnA;
                case "DMnA":
                    return Role.DMnA;
                case "DMnE":
                    return Role.DMnE;
                case "EnA":
                    return Role.EnA;
                case "E":
                    return Role.E;
                case "A":
                    return Role.A;
                case "DM":
                    return Role.DM;

                default:
                    return Role.None;
            }
        }











    }
}
