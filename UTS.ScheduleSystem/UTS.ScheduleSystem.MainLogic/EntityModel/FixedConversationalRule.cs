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

        public FixedConversationalRule(string id, string input, string output, string relatedUsersId, string status) : base(id, input, output, relatedUsersId, status)
        {

        }

        public FixedConversationalRule()
        {

        }

        public string Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }

        [Required]
        [StringLength(128)]
        public string Input
        {
            get
            {
                return base.Input;
            }

            set
            {
                base.Input = value;
            }
        }

        [Required]
        [StringLength(128)]
        public string Output
        {
            get
            {
                return base.Output;
            }

            set
            {
                base.Output = value;
            }
        }

        [StringLength(128)]
        public string RelatedUsersId
        {
            get
            {
                return base.RelatedUsersId;
            }

            set
            {
                base.RelatedUsersId = value;
            }
        }

        [Required]
        [StringLength(128)]
        public string Status
        {
            get
            {
                return base.Status;
                //return Utils.GetStatus(base.status);
            }

            set
            {
                base.Status = value;
                //base.status = value.ToString();
            }
        }
    }
}
