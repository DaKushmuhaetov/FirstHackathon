using System;

namespace FirstHackathon.Models.Votes
{
    /// <summary>
    /// Ответ на голосование <see cref="Voting"/> от конкретного жильца
    /// </summary>
    public sealed class Vote
    {
        public Guid Id { get; }
        public Person Person { get; }
        public Variant Variant { get; }

        private Vote() { }
        public Vote(Guid id, Person person, Variant variant)
        {
            Id = id;
            Person = person ?? throw new ArgumentNullException(nameof(person));
            Variant = variant ?? throw new ArgumentNullException(nameof(variant));
        }
    }
}
