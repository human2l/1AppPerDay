namespace UTS.ScheduleSystem.MainLogic
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

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<ConversationalRule> ConversationalRules { get; set; }
        public virtual DbSet<FixedConversationalRule> FixedConversationalRules { get; set; }
        public virtual DbSet<MealSchedule> MealSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Role)
                .IsUnicode(false);
        }

    }
}
