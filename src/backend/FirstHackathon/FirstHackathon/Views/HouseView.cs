using System;

namespace FirstHackathon.Views
{
    public sealed class HouseView
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public int? LivesHereCounter { get; set; }
    }
}
