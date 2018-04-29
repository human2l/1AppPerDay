using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    class DataHandler
    {
        public DataHandler()
        {

        }

        public void AddConversationalRule()
        {
            
        }

        public void RemoveConversationalRule()
        {

        }

        public void ChangeOnConversationalRule()
        {

        }

        public string FindSingleConversationalRule(string input)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public List<ConversationalRule> FindConversationalRulesAccordingToStatus(Status status)
        {
            List<ConversationalRule> result = new List<ConversationalRule>();
            return result;
        }

        public Object GetInputFromConversationalRule()
        {
            Object inputColumn = new Object();
            return inputColumn;
        }

        public void AddFixedConversationalRule()
        {

        }

        public void RemoveFixedConversationalRule()
        {

        }

        public void ChangeOnFixedConversationalRule()
        {

        }

        public string FindSingleFixedConversationalRule(string input)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public List<FixedConversationalRule> FindFixedConversationalRulesAccordingToStatus(Status status)
        {
            List<FixedConversationalRule> result = new List<FixedConversationalRule>();
            return result;
        }

        public void FindFixedConversationalRules()
        {

        }

        public void AddMealschedule()
        {

        }

        public void RemoveMealschedule()
        {

        }

        public void ChangeOnMealschedule()
        {

        }

        public string FindSingleMealschedule(string inputKeyword, string outputKeyword, string parameter)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public void FindMealschedules()
        {

        }
    }
}
