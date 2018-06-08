using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.WebMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UTS.ScheduleSystem.WebMVC.Controllers.Tests
{
    [TestClass()]
    public class HomeControllermvcTests
    {
        [TestMethod()]
        public void IndexHomeTest()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}