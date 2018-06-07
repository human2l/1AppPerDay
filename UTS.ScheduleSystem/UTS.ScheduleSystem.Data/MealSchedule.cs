namespace UTS.ScheduleSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MealSchedule")]
    public partial class MealSchedule
    {
        public MealSchedule()
        {

        }

        public int Id { get; set; }

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
