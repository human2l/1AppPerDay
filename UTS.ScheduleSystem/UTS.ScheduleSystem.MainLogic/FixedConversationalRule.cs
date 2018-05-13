namespace UTS.ScheduleSystem.MainLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixedConversationalRule")]
    public partial class FixedConversationalRule : Rule
    {
        public FixedConversationalRule(string id, string input, string output, string relatedUsersId, Status status) : base(id, input, output, relatedUsersId, status)
        {

        }
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Input
        {
            get
            {
                return base.input;
            }

            set
            {
                base.input = value;
            }
        }

        [Required]
        [StringLength(128)]
        public string Output { get; set; }

        [StringLength(128)]
        public string RelatedUsersId { get; set; }

        [Required]
        [StringLength(128)]
        public Status Status
        {
            get
            {
                return Utils.GetStatus(base.status);
            }

            set
            {
                status = value.ToString();
            }
        }
    }
}
