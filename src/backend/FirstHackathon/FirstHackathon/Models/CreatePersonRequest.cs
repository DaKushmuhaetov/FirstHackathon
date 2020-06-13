using System;

namespace FirstHackathon.Models
{
    public sealed class CreatePersonRequest
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public string Login { get; }
        public string Password { get; }
        public House House { get; }

        private CreatePersonRequest() { }
        public CreatePersonRequest(Guid id, string name, string surname, string login, string password, House house)
        {
            Id = id;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));

            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));

            House = house ?? throw new ArgumentNullException(nameof(house));
        }
    }
}
