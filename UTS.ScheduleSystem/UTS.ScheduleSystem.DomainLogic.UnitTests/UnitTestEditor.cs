using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestEditor
    {
        private static EditorService editorService = new EditorService();
        private Boolean CompareTwoRules(Rule rule1, Rule rule2)
        {
            Boolean isSame =
                (rule1.Input.Equals(rule2.Input) &&
                rule1.Output.Equals(rule2.Output) &&
                rule1.RelatedUsersId.Equals(rule2.RelatedUsersId) &&
                rule1.Status.Equals(rule2.Status)) ? true : false;
            return isSame;
        }

        [TestMethod]
        public void TestMethod1()
        {
            FixedConversationalRule cFRule1 = new FixedConversationalRule("q", "q", "u001", "Pending");
            editorService.AddNewFCRule("q", "q", "u001");

            Assert.IsTrue(CompareTwoRules(cFRule1, cFRule1));
        }
    }
}
