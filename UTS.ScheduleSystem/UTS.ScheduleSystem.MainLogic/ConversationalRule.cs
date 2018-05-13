namespace UTS.ScheduleSystem.MainLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConversationalRule")]
    public partial class ConversationalRule : Rule
    {
        public ConversationalRule(string id, string input, string output, string relatedUsersId, Status status) : base(id, input, output, relatedUsersId, status)
        {

        }

        //public string Id { get; set; }
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
        //public string Input { get; set; }
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
        //public string Output { get; set; }
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
        //public string RelatedUsersId { get; set; }
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
        [StringLength(128)]
        //public Status Status { get; set; }
        public Status Status
        {
            get
            {
                return base.status;
            }

            set
            {
                base.status = value;
            }
        }
    }
}
