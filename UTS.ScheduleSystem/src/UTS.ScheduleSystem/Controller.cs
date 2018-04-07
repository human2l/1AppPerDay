using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class Controller
    {
        //dummy code for test
        User frank = new User("u001", "Frank", "frank", "frank@frank.com", Role.DMnA);
        ConversationalRule weatherRule1 = new ConversationalRule("c001", "Weather", "How is the weather on ", "The weather on {p1} is {p2}", "u001 u002", Status.Pending);
        FixedConversationalRule weatherFRule1 = new FixedConversationalRule("c002", "Greeting", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
        FakeDB fakeDB = new FakeDB();
        



    }
}
