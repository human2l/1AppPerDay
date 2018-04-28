using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTS.ScheduleSystem.MainLogic;

namespace UTS.ScheduleSystem.Web
{
    public partial class _Default : Page
    {
        private Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = new Controller();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            answer.Text = controller.ConversationService.Conversation(question.Text);
        }
    }
}