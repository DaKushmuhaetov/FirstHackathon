using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstHackathon.Views
{
    public sealed class ClosedVotingView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<VariantView> Variants { get; set; }
        public bool IsClosed { get; set; }
        
    }
}
