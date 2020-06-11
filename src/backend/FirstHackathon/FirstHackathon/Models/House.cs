using System;
using System.Collections.Generic;

namespace FirstHackathon.Models
{
    public sealed class House
    {
        public Guid Id { get; }
        public string Address { get; }
        public List<Person> People { get; }

        private House() { }
        public House(string address, List<Person> people)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            People = people ?? new List<Person>();
        }
    }
}
