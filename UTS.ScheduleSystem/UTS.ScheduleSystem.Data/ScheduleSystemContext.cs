namespace UTS.ScheduleSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ScheduleSystemContext : DbContext
    {
        public ScheduleSystemContext()
            : base("name=ScheduleSystemContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
