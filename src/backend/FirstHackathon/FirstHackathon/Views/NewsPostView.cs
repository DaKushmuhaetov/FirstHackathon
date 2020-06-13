using System;

namespace FirstHackathon.Views
{
    public sealed class NewsPostView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Image { get; set; }
    }
}
