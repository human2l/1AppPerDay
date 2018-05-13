namespace UTS.ScheduleSystem.MainLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MealSchedule")]
    public partial class MealSchedule
    {
        public MealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditUserId)
        {
            this.Id = id;
            this.Topic = topic;
            this.Participants = participants;
            this.Location = location;
            this.StartDate = startDate;
            this.EndDate = EndDate;
            this.LastEditUserId = lastEditUserId;
        }

        public string Id { get; set; }

        [StringLength(128)]
        public string Topic { get; set; }

        [StringLength(128)]
        public string Participants { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(128)]
        public string StartDate { get; set; }

        [StringLength(128)]
        public string EndDate { get; set; }

        [Required]
        [StringLength(128)]
        public string LastEditUserId { get; set; }
    }
}
