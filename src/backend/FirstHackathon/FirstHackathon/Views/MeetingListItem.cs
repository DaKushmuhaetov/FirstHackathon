using System;

namespace FirstHackathon.Views
{
    public sealed class MeetingListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Description { get; set; }
    }
}
