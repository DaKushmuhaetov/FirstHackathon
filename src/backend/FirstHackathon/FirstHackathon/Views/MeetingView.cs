using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstHackathon.Views
{
    public sealed class MeetingView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Description { get; set; }
        public HouseView House { get; set; }
    }
}
