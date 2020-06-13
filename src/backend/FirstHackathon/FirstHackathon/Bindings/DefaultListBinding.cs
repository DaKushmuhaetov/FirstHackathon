using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class DefaultListBinding
    {
        /// <summary>
        /// Offset for pagination. Optional. 0 by default.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Number of items per page. Optional. 20 by default.
        /// </summary>
        public int Limit { get; set; } = 20;
    }

    public sealed class DefaultListBindingValidator : AbstractValidator<DefaultListBinding>
    {
        public DefaultListBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}
