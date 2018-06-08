using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.WebMVC.Controllers;

namespace UTS.ScheduleSystem.WebMVC.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void MaintainData_AllMealSchedulesInDatabase_ReturnsAll()
        //{
        //    var controller = new DataMaintainerController();
        //    var result = (ViewResult)controller.Index();
        //}

        [TestMethod]
        public void Home_InputQuestion_ReturnsQuestion()
        {
            var controller = new HomeController();
            WebMVC
            ViewResult
            var result = controller.Index() as ViewResult;

            var model = (Question)result.Model;
        }
    }
}
