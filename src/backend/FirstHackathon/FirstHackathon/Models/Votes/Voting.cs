using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstHackathon.Models.Votes
{
    public sealed class Voting
    {
        public Guid Id { get; }

        public string Title { get; }

        /// <summary>
        /// Варианты ответов
        /// </summary>
        public List<Variant> Variants { get; } = new List<Variant>();

        public House House { get; }

        public bool IsClosed { get; private set; } = false;

        private Voting() { }
        public Voting(Guid id, string title, House house)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            House = house ?? throw new ArgumentNullException(nameof(house));
        }

        public void Vote(Guid variantId, Person person)
        {
            var variant = Variants.SingleOrDefault(o => o.Id == variantId);
            variant.Vote(new Votes.Vote(Guid.NewGuid(), person, variant));
        }

        public void UnVote(Guid variantId, Person person)
        {
            var variant = Variants.SingleOrDefault(o => o.Id == variantId);
            variant.UnVote(person.Id);
        }

        public void AddVariant(Variant variant)
        {
            if (IsClosed)
                throw new InvalidOperationException($"IsClosed was: {IsClosed}");

            if (variant == null)
                throw new ArgumentNullException(nameof(variant));

            Variants.Add(variant);
        }

        public void ClosedVote()
        {
            IsClosed = true;
        }

        public void RemoveVariant(Guid variantId)
        {
            if (IsClosed)
                throw new InvalidOperationException($"IsClosed was: {IsClosed}");

            Variants.Remove(Variants.SingleOrDefault(o => o.Id == variantId));
        }
    }
}
