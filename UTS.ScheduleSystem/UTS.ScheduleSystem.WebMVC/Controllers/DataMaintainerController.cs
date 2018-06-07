using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class DataMaintainerController : Controller
    {
        // GET: DataMaintainer
        // Show the list of all contacts
        public ActionResult Index()
        {
            ViewBag.MealSchedules = MealScheduleHandler.FindAllMealSchedules();
            return View();
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
            //need changed!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            mealSchedule.LastEditUserId = "111";
            MealScheduleHandler.AddMealschedule(mealSchedule);
            return RedirectToAction("Index");
        }

        // GET: MealSchedules/Edit/5
        // Show an edit form to edit an existing mealSchedule
        public ActionResult Edit(int? id)
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
            ViewBag.OnEditingMealSchedule = mealSchedule;
            return View();
        }

        // POST: MealSchedules/Edit/5
        // Save changes to an edited mealSchedule
        [HttpPost]
        public ActionResult Edit(MealSchedule mealSchedule)
        {
            //need changed!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1111
            mealSchedule.LastEditUserId = "111";
            MealScheduleHandler.UpdateAMealschedule(mealSchedule);
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
            var mealSchedule = MealScheduleHandler.FindMealScheduleById(id + "");
            if (mealSchedule == null)
            {
                return HttpNotFound();
            }
            MealScheduleHandler.RemoveMealschedule(mealSchedule);
            return RedirectToAction("Index");
        }


    }
}
