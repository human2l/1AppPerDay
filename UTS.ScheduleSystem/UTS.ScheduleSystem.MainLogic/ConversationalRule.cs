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

        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Input { get; set; }

        [Required]
        [StringLength(128)]
        public string Output { get; set; }

        [StringLength(128)]
        public string RelatedUsersId { get; set; }

        [Required]
        [StringLength(128)]
        public Status Status { get; set; }
    }
}
