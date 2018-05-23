﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.WebMVC.Models;


namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Answer = "I'm waiting...";
            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeControllerViewModels models)
        {
            if (models.Question != null)
            {
                //ViewBag.Answer = "find answer of input: " + models.Question;
                ViewBag.Answer = HandleConversation(models.Question);
            }
            else
            {
                ViewBag.Answer = "Please type something!";
            }

            return View();

        }

        private string HandleConversation(string input)
        {
            //string output = "";
            input = MainLogic.Utils.RemoveAllMarks(input);
            input = MainLogic.Utils.IgnoreWhiteSpace(input);
            //string keyword = "";
            //Rule rule = new Rule();
            //string[] inputSplitedRule = MainLogic.ConversationService.SplitRule(input);
            MainLogic.ConversationService conversationService = new MainLogic.ConversationService();
            string output = conversationService.Conversation(input);
            //string output = MainLogic.ConversationService.Conversation(input);

            return output;
        }

    }
}