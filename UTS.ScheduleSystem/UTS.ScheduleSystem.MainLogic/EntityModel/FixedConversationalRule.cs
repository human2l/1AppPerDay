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
        private int id;
        public FixedConversationalRule(int id, string input, string output, string relatedUsersId, string status)
        {
            this.id = id;
            base.input = input;
            base.output = output;
            base.relatedUsersId = relatedUsersId;
            base.status = status;
        }

        public FixedConversationalRule()
        {

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {

                return id;
            }

            set
            {
                id = value;
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
