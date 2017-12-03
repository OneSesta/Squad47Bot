using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    internal class Person
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Patronymic { get; private set; }
        public string PhoneNumber { get; private set; }

        public Person(string lastName, string firstName, string patronymic, string phoneNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
        }
    }

}
