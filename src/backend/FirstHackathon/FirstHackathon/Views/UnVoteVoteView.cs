using FirstHackathon.Models.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstHackathon.Views
{
    public sealed class UnVoteVoteView
    {
        public Guid Id { get; set; }
        public VotingView VotingView { get; set; }
    }
}
