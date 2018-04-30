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
        private User currentUser;
        private List<User> userList = new List<User>();
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
                //var adapter = new ConversationalRuleTableAdapter();
                //var set = adapter.GetData();

                //for (var i = 0; i < set.Count; i++)
                //{
                //    adapter.Delete(set[i].Id, set[i].Input, set[i].Output, set[i].RelatedUsersId, set[i].Status);
                //}
                //System.Diagnostics.Debug.WriteLine("valueCount: " + value.Count);
                //for (var i = 0; i < value.Count; i++)
                //{
                //    ConversationalRule ms = value[i];
                //    System.Diagnostics.Debug.WriteLine(ms.Id);
                //    adapter.Insert(ms.Id, ms.Input, ms.Output, ms.RelatedUsersId, ms.Status.ToString());
                //}
                //adapter.Dispose();
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
                List<FixedConversationalRule> list = new List<FixedConversationalRule>();
                fixedConversationalRulesList = list;
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
                //var adapter = new FixedConversationalRuleTableAdapter();
                //var set = adapter.GetData();
                //for (var i = 0; i < set.Count; i++)
                //{
                //    adapter.Delete(set[i].Id, set[i].Input, set[i].Output, set[i].RelatedUsersId, set[i].Status);
                //}
                //for (var i = 0; i < value.Count; i++)
                //{
                //    FixedConversationalRule rule = value[i];
                //    adapter.Insert(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
                //}
                //adapter.Dispose();
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
                //var adapter = new MealScheduleTableAdapter();
                //var set = adapter.GetData();

                //for (var i = 0; i < set.Count; i++)
                //{
                //    adapter.Delete(set[i].Id, set[i].Topic, set[i].Participants, set[i].Location, set[i].StartDate, set[i].EndDate, set[i].LastEditUserId);
                //}
                //System.Diagnostics.Debug.WriteLine("valueCount: " + value.Count);
                //for (var i = 0; i < value.Count; i++)
                //{
                //    MealSchedule ms = value[i];
                //    System.Diagnostics.Debug.WriteLine(ms.Id);
                //    adapter.Insert(ms.Id, ms.Topic, ms.Participants, ms.Location, ms.StartDate, ms.EndDate, ms.LastEditUserId); ;
                //}
                //adapter.Dispose();
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

        public ConversationService ConversationService { get => conversationService; set => conversationService = value; }

    }
}
