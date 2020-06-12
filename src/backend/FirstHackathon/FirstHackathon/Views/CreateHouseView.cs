using System;

namespace FirstHackathon.Views
{
    public sealed class CreateHouseView
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public int? LivesHereCounter { get; set; }
        public TokenView Token { get; set; }
    }
}
