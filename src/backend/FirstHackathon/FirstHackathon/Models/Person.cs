using FirstHackathon.Models.Votes;
using System;
using System.Collections.Generic;

namespace FirstHackathon.Models
{
    public sealed class Person
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public string Login { get; }
        public string Password { get; private set; }
        public House House { get; }
        public List<Vote> Votes { get; } = new List<Vote>();

        private Person() { }
        public Person(Guid id, string name, string surname, string login, string password, House house)
        {
            Id = id;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));

            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));

            House = house;
        }

        public void SetPassword(string newPassword)
        {
            if (newPassword == null)
                return;

            Password = newPassword;
        }
    }
}
