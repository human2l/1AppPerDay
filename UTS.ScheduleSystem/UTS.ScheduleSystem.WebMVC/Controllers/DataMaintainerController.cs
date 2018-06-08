using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class DataMaintainerController : Controller
    {
        private DataMaintainerService dataMaintainerService = new DataMaintainerService();
        private string currentUser = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: DataMaintainer
        // Show the list of all contacts
        public ActionResult Index()
        {
            ViewBag.MealSchedules = MealScheduleHandler.FindAllMealSchedules();
            return CheckCurrentUser();
        }

        // GET: DataMaintainer/Create
        // Show an edit form to create a new contact
        public ActionResult Create()
        {
            return CheckCurrentUser();
        }

        // POST: DataMaintainer/Create
        // Create a MealSchedule based on the supplied data
        [HttpPost]
        public ActionResult Create([Bind(Include = "Topic,Location,Participants,StartDate,EndDate")] MealSchedule mealSchedule)
        {
            mealSchedule.LastEditUserId = currentUser;
            if (dataMaintainerService.IsDataValid(mealSchedule))
            {
                MealSchedule newMealSchedule = dataMaintainerService.MealScheduleFormat(mealSchedule);
                dataMaintainerService.AddMealSchedule(newMealSchedule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                Response.Write("<script>window.alert('Invalid Input. Please edit again.');</script>");
                return Create();
            }
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
            return CheckCurrentUser();
        }

        // POST: MealSchedules/Edit/5
        // Save changes to an edited mealSchedule
        [HttpPost]
        public ActionResult Edit(MealSchedule mealSchedule)
        {
            mealSchedule.LastEditUserId = currentUser;
            if (dataMaintainerService.IsDataValid(mealSchedule))
            {
                MealSchedule newMealSchedule = dataMaintainerService.MealScheduleFormat(mealSchedule);
                dataMaintainerService.EditMealSchedule(newMealSchedule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                Response.Write("<script>window.alert('Invalid Input. Please edit again.');</script>");
                return Edit(mealSchedule.Id);
            }
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
            return CheckCurrentUser();
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
            dataMaintainerService.DeleteMealSchedule(mealSchedule);
            return RedirectToAction("Index");
        }

        // Check the role of current user
        public ActionResult CheckCurrentUser()
        {
            if (currentUser != "" && UserHandler.GetCurrentUserRole(currentUser).Contains("DM"))
            {
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

    }
}
