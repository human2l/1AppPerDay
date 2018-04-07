using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public enum Role { DM, E, A, DMnE, DMnA, EnA, DMnEnA}
    public class User
    {
        private string id;
        private string name;
        private string password;
        private string email;
        private Role role;

        public User(string id, string name, string password, string email, Role role)
        {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
            Role = role;
        }

        public string Id
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
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public Role Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

       
    }
}
