using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class GetHouseListBinding
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

    public sealed class GetHouseListBindingValidator : AbstractValidator<GetHouseListBinding>
    {
        public GetHouseListBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}
