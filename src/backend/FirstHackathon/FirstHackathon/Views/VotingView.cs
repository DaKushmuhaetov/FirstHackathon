using System;
using System.Collections.Generic;

namespace FirstHackathon.Views
{
    public sealed class VotingView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<VariantView> Variants { get; set; }
    }
}
