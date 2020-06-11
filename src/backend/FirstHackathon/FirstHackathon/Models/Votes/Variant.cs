using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstHackathon.Models.Votes
{
    /// <summary>
    /// Вариант ответа для голосования <see cref="Voting"/>
    /// </summary>
    public sealed class Variant
    {
        public Guid Id { get; }
        public string Title { get; }
        public Voting Voting { get; }
        public List<Vote> Votes { get; } = new List<Vote>();

        private Variant() { }
        public Variant(Guid id, string title, Voting voting)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Voting = voting ?? throw new ArgumentNullException(nameof(voting));
        }

        public void Vote(Vote vote)
        {
            if (vote == null)
                throw new ArgumentNullException(nameof(vote));

            Votes.Add(vote);
        }

        public void UnVote(Guid voteId)
        {
            Votes.Remove(Votes.SingleOrDefault(o => o.Id == voteId));
        }
    }
}
