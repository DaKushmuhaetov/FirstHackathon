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
        public List<Variant> Variants { get; }

        public House House { get; }

        public bool IsClosed { get; private set; } = false;

        private Voting() { }
        public Voting(Guid id, string title, List<Variant> variants, House house)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Variants = variants ?? throw new ArgumentNullException(nameof(variants));
            House = house ?? throw new ArgumentNullException(nameof(house));
        }

        public void AddVariant(Variant variant)
        {
            if (IsClosed)
                throw new InvalidOperationException($"IsClosed was: {IsClosed}");

            if (variant == null)
                throw new ArgumentNullException(nameof(variant));

            Variants.Add(variant);
        }

        public void RemoveVariant(Guid variantId)
        {
            if (IsClosed)
                throw new InvalidOperationException($"IsClosed was: {IsClosed}");

            Variants.Remove(Variants.SingleOrDefault(o => o.Id == variantId));
        }
    }
}
