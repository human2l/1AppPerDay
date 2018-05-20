using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.WebMVC.Models
{
    public class EditorViewModels
    {
        public List<ConversationalRule> conversionalRule = new List<ConversationalRule>();
        public List<FixedConversationalRule> fixedConversionalRule = new List<FixedConversationalRule>();

        public int Id { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public string RelatedUsersId { get; set; }

        public string Status { get; set; }

    }
}