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

        private Voting() { }
        public Voting(Guid id, string title, List<Variant> variants)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Variants = variants ?? throw new ArgumentNullException(nameof(variants));
        }

        public void AddVariant(Variant variant)
        {
            if (variant == null)
                throw new ArgumentNullException(nameof(variant));

            Variants.Add(variant);
        }

        public void RemoveVariant(Guid variantId)
        {
            Variants.Remove(Variants.SingleOrDefault(o => o.Id == variantId));
        }
    }
}
