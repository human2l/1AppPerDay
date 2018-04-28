using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem.MainLogic
{
    public class Controller
    {
        private FakeDB fakeDB = new FakeDB();
        private User currentUser;
        private List<User> userList = new List<User>();
        private List<ConversationalRule> conversationalRulesList = new List<ConversationalRule>();
        private List<FixedConversationalRule> fixedConversationalRulesList = new List<FixedConversationalRule>();
        private List<MealSchedule> mealScheduleList = new List<MealSchedule>();

        //private ConversationService
        private ConversationService conversationService = new ConversationService();
        private DataMaintainerService dataMaintainerService = new DataMaintainerService();
        private EditorService editorService = new EditorService();
        private ApproverService approverService = new ApproverService();

         

        
        public Controller()
        {
            //initialization();
            
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
                List<User> u = new List<User>();

                while (set.Count > 0)
                {
                    User user = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, Utils.GetRole(set.First().Role));
                    set.RemoveAspNetUsersRow(set.First());
                    u.Add(user);
                }

                userList = u;

                //if(set.Count >= 1)
                //{
                //    User DMnEnA = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                //    set.RemoveAspNetUsersRow(set.First());
                //    userList.Add(DMnEnA);
                //}
                //if(set.Count >= 3)
                //{
                //    User DM = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                //    set.RemoveAspNetUsersRow(set.First());
                //    User E = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                //    set.RemoveAspNetUsersRow(set.First());
                //    User A = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, GetRole(set.First().Role));
                //    set.RemoveAspNetUsersRow(set.First());
                //    userList.Add(DM);
                //    userList.Add(E);
                //    userList.Add(A);
                //}
                
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
                List<ConversationalRule> list = new List<ConversationalRule>();
                conversationalRulesList = list;
                //for (var i = 0; i < conversationalRulesList.Count; i++)
                //{
                //    conversationalRulesList.RemoveAt(i);
                //}
                while (set.Count != 0)
                {
                    ConversationalRule cRule = new ConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                        Status.Pending);
                    set.RemoveConversationalRuleRow(set.First());
                    conversationalRulesList.Add(cRule);
                }
                return conversationalRulesList;
            }

            set
            {
                var adapter = new ConversationalRuleTableAdapter();
                var set = adapter.GetData();

                for (var i = 0; i < set.Count; i++)
                {
                    adapter.Delete(set[i].Id, set[i].Input, set[i].Output, set[i].RelatedUsersId, set[i].Status);
                }
                System.Diagnostics.Debug.WriteLine("valueCount: " + value.Count);
                for (var i = 0; i < value.Count; i++)
                {
                    ConversationalRule ms = value[i];
                    System.Diagnostics.Debug.WriteLine(ms.Id);
                    adapter.Insert(ms.Id, ms.Input, ms.Output, ms.RelatedUsersId, ms.Status.ToString());
                }
                adapter.Dispose();
                //conversationalRulesList = value;
            }
        }

        public List<FixedConversationalRule> FixedConversationalRulesList
        {
            get
            {
                var adapter = new FixedConversationalRuleTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
                List<FixedConversationalRule> list = new List<FixedConversationalRule>();
                fixedConversationalRulesList = list;
                //for (var i = 0; i < fixedConversationalRulesList.Count; i++)
                //{
                //    fixedConversationalRulesList.RemoveAt(i);
                //}
                while (set.Count != 0)
                {
                    FixedConversationalRule fcRule = new FixedConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                        Status.Pending);
                    set.RemoveFixedConversationalRuleRow(set.First());
                    fixedConversationalRulesList.Add(fcRule);
                }
                return fixedConversationalRulesList;
            }

            set
            {
                var adapter = new FixedConversationalRuleTableAdapter();
                var set = adapter.GetData();
                for (var i = 0; i < set.Count; i++)
                {
                    adapter.Delete(set[i].Id, set[i].Input, set[i].Output, set[i].RelatedUsersId, set[i].Status);
                }
                for (var i = 0; i < value.Count; i++)
                {
                    FixedConversationalRule rule = value[i];
                    adapter.Insert(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
                }
                adapter.Dispose();
                //fixedConversationalRulesList = value;
            }
        }

        public List<MealSchedule> MealScheduleList
        {
            get
            {
                var adapter = new MealScheduleTableAdapter();
                var set = adapter.GetData();
                adapter.Dispose();
                //for (var i = 0; i < mealScheduleList.Count; i++)
                //{
                //    mealScheduleList.RemoveAt(i);
                //}
                mealScheduleList = new List<MealSchedule>();
                while (set.Count != 0)
                {
                    MealSchedule ms = new MealSchedule(set.First().Id, set.First().Topic, set.First().Participants, set.First().Location, set.First().StartDate, set.First().EndDate, set.First().LastEditUserId);
                    set.RemoveMealScheduleRow(set.First());

                    mealScheduleList.Add(ms);
                }

                return mealScheduleList;
            }

            set
            {
                var adapter = new MealScheduleTableAdapter();
                var set = adapter.GetData();

                for (var i = 0; i < set.Count; i++)
                {
                    adapter.Delete(set[i].Id, set[i].Topic, set[i].Participants, set[i].Location, set[i].StartDate, set[i].EndDate, set[i].LastEditUserId);
                }
                System.Diagnostics.Debug.WriteLine("valueCount: " + value.Count);
                for (var i = 0; i < value.Count; i++)
                {
                    MealSchedule ms = value[i];
                    System.Diagnostics.Debug.WriteLine(ms.Id);
                    adapter.Insert(ms.Id, ms.Topic, ms.Participants, ms.Location, ms.StartDate, ms.EndDate, ms.LastEditUserId); ;
                }
                adapter.Dispose();
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

        public ConversationService ConversationService { get => conversationService; set => conversationService = value; }

        //public void initialization()
        //{
        //    User frank = new User("u001", "Frank", "frank", "frank@frank.com", Role.DMnA);
        //    ConversationalRule weatherRule1 = new ConversationalRule("c001", "How is the weather on ", "The weather on {p1} is {p2}", "u001 u002", Status.Approved);
        //    FixedConversationalRule weatherFRule1 = new FixedConversationalRule("fc001", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Rejected);
        //    FixedConversationalRule weatherFRule2 = new FixedConversationalRule("fc002", "qwerty", "asdfgg", "u001", Status.Pending);
        //    MealSchedule mealSchedule1 = new MealSchedule("ms1","u001", "dinner", "Michael Bay,Donald Trump", "Sydney", "08/04/2018 3:12:18 PM", "08/04/2018 4:15:00 PM", "u001 u002");
        //    MealSchedule ms1 = new MealSchedule("ms2", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
        //    MealSchedule ms2 = new MealSchedule("ms3", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
        //    //fakeDB.UserTbl.Add(frank);
        //    //fakeDB.CRulesTbl.Add(weatherRule1);
        //    //fakeDB.FCRulesTbl.Add(weatherFRule1);
        //    //fakeDB.MealScheduleTbl.Add(mealSchedule1);
        //    UserList.Add(frank);
        //    conversationalRulesList.Add(weatherRule1);
        //    fixedConversationalRulesList.Add(weatherFRule1);
        //    fixedConversationalRulesList.Add(weatherFRule2);
        //    MealScheduleList.Add(mealSchedule1);
        //    MealScheduleList.Add(ms1);
        //    MealScheduleList.Add(ms2);


        //    //codes for read database and update all lists


        //}

        //main process
        public void maintaining()
        {

        }


        public bool handleConversation(string userInput)
        {
            return false;
        }

        











    }
}
