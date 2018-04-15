using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private DMService dataMaintainerService = new DMService();
        private EditorService editorService = new EditorService();
        private ApproverService approverService = new ApproverService();

        private Utils utils = new Utils();
        

        

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

        public DMService DataMaintainerService
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
                return mealScheduleList;
            }

            set
            {
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

        public Utils Utils
        {
            get
            {
                return utils;
            }

            set
            {
                utils = value;
            }
        }

        public void initialization()
        {
            User frank = new User("u001", "Frank", "frank", "frank@frank.com", Role.DMnA);
            ConversationalRule weatherRule1 = new ConversationalRule("c001", "How is the weather on ", "The weather on {p1} is {p2}", "u001 u002", Status.Pending);
            FixedConversationalRule weatherFRule1 = new FixedConversationalRule("fc002", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            MealSchedule mealSchedule1 = new MealSchedule("m001","u001", "dinner", "Michael Bay,Donald Trump", "Sydney", "08/04/2018 3:12:18 PM", "08/04/2018 4:15:00 PM", "u001 u002");
            fakeDB.UserTbl.Add(frank);
            fakeDB.CRulesTbl.Add(weatherRule1);
            fakeDB.FCRulesTbl.Add(weatherFRule1);
            fakeDB.MealScheduleTbl.Add(mealSchedule1);

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













    }
}
