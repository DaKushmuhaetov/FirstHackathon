using System;

namespace FirstHackathon.Models
{
    public sealed class Person
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public string Login { get; }
        public string Password { get; private set; }

        public Guid HouseId { get; }
        public House House { get; }

        private Person() { }
        public Person(string name, string surname, string login, string password, Guid houseId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));

            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));

            HouseId = houseId;
        }

        public void SetPassword(string newPassword)
        {
            if (newPassword == null)
                return;

            Password = newPassword;
        }
    }
}
