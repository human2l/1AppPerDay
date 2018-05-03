using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public enum Role { DM, E, A, DMnE, DMnA, EnA, DMnEnA, None}
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

        public User()
        {

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
