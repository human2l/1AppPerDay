using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class DataMaintainerController : Controller
    {
        // GET: DataMaintainer
        // Show the list of all contacts
        public ActionResult Index()
        {
            //List<MealSchedule> mealSchedules = new List<MealSchedule>();
            //MealSchedule m1 = new MealSchedule();
            //m1.Id = 1;
            //m1.Topic = "t";
            //m1.Location = "l";
            //m1.Participants = "p";
            //m1.StartDate = "sd";
            //m1.EndDate = "ed";
            //m1.LastEditUserId = "leid";
            //mealSchedules.Add(m1);

            ViewBag.MealSchedules = MealScheduleHandler.FindAllMealSchedules();
            //viewbag:
            //ViewBag.MealSchedules = mealSchedules;
            return View();
            //model:
            //
        }

        // GET: DataMaintainer/Create
        // Show an edit form to create a new contact
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataMaintainer/Create
        // Create a MealSchedule based on the supplied data
        [HttpPost]
        public ActionResult Create([Bind(Include = "Topic,Location,Participants,StartDate,EndDate")] MealSchedule mealSchedule)
        {
            mealSchedule.LastEditUserId = "asdf";
            MealScheduleHandler.AddMealschedule(mealSchedule);
            return RedirectToAction("Index");
        }

        // GET: DataMaintainer/Delete/5
        // Retrieve details of a contact to confirm deletion
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mealSchedule = MealScheduleHandler.FindMealScheduleById(id+"");
            if (mealSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnDeletingMealSchedule = mealSchedule;
            return View();
        }
        // POST: MealSchedules/Delete/5
        // Delete a mealSchedule
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var mealSchedule = MealScheduleHandler.FindMealScheduleById(id+"");
            if (mealSchedule == null)
            {
                return HttpNotFound();
            }
            MealScheduleHandler.RemoveMealschedule(mealSchedule);
            return RedirectToAction("Index");
        }


    }
}
