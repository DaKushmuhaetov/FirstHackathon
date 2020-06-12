using System;

namespace FirstHackathon.Models
{
    public sealed class Meeting
    {
        public Guid Id { get; }
        public string Title { get; }
        public DateTime MeetingDate { get; }
        public string Description { get; }
        public byte[] Image { get; }
        public House House { get; }

        private Meeting() { }
        public Meeting(Guid id, string title, DateTime meetingDate, string description, byte[] image, House house)
        {
            Id = id;

            Title = title ?? throw new ArgumentNullException(nameof(title));

            MeetingDate = meetingDate;

            Description = description;

            Image = image;

            House = house ?? throw new ArgumentNullException(nameof(house));
        }
    }
}
