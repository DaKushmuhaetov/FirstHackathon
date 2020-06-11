﻿using System;

namespace FirstHackathon.Views
{
    public sealed class HouseListItem
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public int LivesHereCounter { get; set; }
    }
}
