using System;

namespace FirstHackathon.Views
{
    public sealed class PersonView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public HouseView House { get; set; }
    }
}
