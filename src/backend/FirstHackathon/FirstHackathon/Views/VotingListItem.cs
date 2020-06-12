using System;
using System.Collections.Generic;

namespace FirstHackathon.Views
{
    public sealed class VotingListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsClosed { get; set; }

        public bool IsVoted { get; set; }
        public List<VariantView> Variants { get; set; }
    }
}
