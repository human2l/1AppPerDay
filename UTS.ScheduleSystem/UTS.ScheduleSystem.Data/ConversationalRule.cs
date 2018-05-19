namespace UTS.ScheduleSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConversationalRule")]
    public partial class ConversationalRule : Rule
    {
        public ConversationalRule(string input, string output, string relatedUsersId, string status) : base(input, output, relatedUsersId, status)
        {

        }

        public ConversationalRule()
        {

        }

        public int Id { get; set; }

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
        public string Status { get; set; }
    }
}
