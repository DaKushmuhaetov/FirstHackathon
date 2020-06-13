using System;

namespace FirstHackathon.Models
{
    public sealed class NewsPost
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public byte[] Image { get; }
        public DateTime CreateDate { get; }
        public House House { get; }

        private NewsPost() { }
        public NewsPost(Guid id, string title, string description, byte[] image, DateTime createDate, House house)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Image = image;
            CreateDate = createDate;
            House = house ?? throw new ArgumentNullException(nameof(house));
        }
    }
}
