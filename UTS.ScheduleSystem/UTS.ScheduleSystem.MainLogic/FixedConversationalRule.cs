namespace UTS.ScheduleSystem.MainLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum Status { Approved, Rejected, Pending }

    [Table("FixedConversationalRule")]
    public partial class FixedConversationalRule : Rule
    {

        public FixedConversationalRule(string id, string input, string output, string relatedUsersId, Status status) : base(id, input, output, relatedUsersId, status)
        {

        }

        public FixedConversationalRule()
        {

        }

        public string Id
        {
            get
            {
                return base.id;
            }

            set
            {
                base.id = value;
            }
        }

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
        public string Output
        {
            get
            {
                return base.output;
            }

            set
            {
                base.output = value;
            }
        }

        [StringLength(128)]
        public string RelatedUsersId
        {
            get
            {
                return base.relatedUsersId;
            }

            set
            {
                base.relatedUsersId = value;
            }
        }

        [Required]
        //[StringLength(128)]
        public Status Status
        {
            get
            {
                return base.status;
                //return Utils.GetStatus(base.status);
            }

            set
            {
                base.status = value;
                //base.status = value.ToString();
            }
        }
    }
}
