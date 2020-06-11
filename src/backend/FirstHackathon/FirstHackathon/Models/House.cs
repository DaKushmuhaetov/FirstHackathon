﻿using FirstHackathon.Models.Votes;
using System;
using System.Collections.Generic;

namespace FirstHackathon.Models
{
    public sealed class House
    {
        public Guid Id { get; }
        public string Address { get; }

        public string Login { get; }
        public string Password { get; }

        public List<Person> People { get; }
        public List<Voting> Votings { get; } = new List<Voting>();

        private House() { }
        public House(Guid id, string address, string login, string password, List<Person> people = null)
        {
            Id = id;

            Address = address ?? throw new ArgumentNullException(nameof(address));

            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));

            People = people ?? new List<Person>();
        }
    }
}
