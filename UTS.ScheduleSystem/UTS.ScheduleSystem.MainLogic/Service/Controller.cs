using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.MainLogic
{
    public enum Status { Approved, Rejected, Pending }
    public enum DataType { Topic, Participants, Location, startDate, endDate }

    public class Controller
    {
        private AspNetUser currentUser;
        private List<AspNetUser> userList = new List<AspNetUser>();
        private List<ConversationalRule> conversationalRulesList = new List<ConversationalRule>();
        private List<FixedConversationalRule> fixedConversationalRulesList = new List<FixedConversationalRule>();
        private List<MealSchedule> mealScheduleList = new List<MealSchedule>();
        private ConversationService conversationService = new ConversationService();
        private DataMaintainerService dataMaintainerService = new DataMaintainerService();
        private EditorService editorService = new EditorService();
        private ApproverService approverService = new ApproverService();
        
        public Controller(){}

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

        public List<AspNetUser> UserList
        {
            get
            {
                //var adapter = new AspNetUsersTableAdapter();
                //var set = adapter.GetData();
                //adapter.Dispose();
                //List<User> u = new List<User>();

                //while (set.Count > 0)
                //{
                //    User user = new User(set.First().Id, set.First().UserName, set.First().PasswordHash, set.First().Email, Utils.GetRole(set.First().Role));
                //    set.RemoveAspNetUsersRow(set.First());
                //    u.Add(user);
                //}
                //userList = u;
                return UserHandler.UsersList();
            }
            //set
            //{
            //    userList = value;
            //}
        }

        public List<ConversationalRule> ConversationalRulesList
        {
            get
            {
                //var adapter = new ConversationalRuleTableAdapter();
                //var set = adapter.GetData();
                //adapter.Dispose();
                //List<ConversationalRule> list = new List<ConversationalRule>();
                //conversationalRulesList = list;
                //while (set.Count != 0)
                //{
                //    ConversationalRule cRule = new ConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                //        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                //        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                //        Status.Pending);
                //    set.RemoveConversationalRuleRow(set.First());
                //    conversationalRulesList.Add(cRule);
                //}
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
                //var adapter = new FixedConversationalRuleTableAdapter();
                //var set = adapter.GetData();
                //adapter.Dispose();
                //List<FixedConversationalRule> list = new List<FixedConversationalRule>();
                //fixedConversationalRulesList = list;
                //while (set.Count != 0)
                //{
                //    FixedConversationalRule fcRule = new FixedConversationalRule(set.First().Id, set.First().Input, set.First().Output, set.First().RelatedUsersId,
                //        (set.First().Status.Equals((Status.Approved).ToString())) ? Status.Approved :
                //        (set.First().Status.Equals((Status.Rejected).ToString())) ? Status.Rejected :
                //        Status.Pending);
                //    set.RemoveFixedConversationalRuleRow(set.First());
                //    fixedConversationalRulesList.Add(fcRule);
                //}
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
                //var adapter = new MealScheduleTableAdapter();
                //var set = adapter.GetData();
                //adapter.Dispose();
                //mealScheduleList = new List<MealSchedule>();
                //while (set.Count != 0)
                //{
                //    MealSchedule ms = new MealSchedule(set.First().Id, set.First().Topic, set.First().Participants, set.First().Location, set.First().StartDate, set.First().EndDate, set.First().LastEditUserId);
                //    set.RemoveMealScheduleRow(set.First());

                //    mealScheduleList.Add(ms);
                //}

                return mealScheduleList;
            }

            set
            {
                mealScheduleList = value;
            }
        }

        public AspNetUser CurrentUser
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

        public ConversationService ConversationService
        {
            get
            {
                return conversationService;
            }

            set
            {
                conversationService = value;
            }
        }

        //public ConversationService ConversationService { get => conversationService; set => conversationService = value; }

    }
}
