using System;
using System.Collections.Generic;

namespace FirstHackathon.Views
{
    public sealed class VariantView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public List<VoteView> Votes { get; set; }
    }
}
