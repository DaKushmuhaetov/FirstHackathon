using FluentValidation;
using System;

namespace FirstHackathon.Bindings
{
    public sealed class GetMeetingsListBinding
    {
        /// <summary>
        /// Offset for pagination. Optional. 0 by default.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Number of items per page. Optional. 20 by default.
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Date from which start counting. Optional.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Date from which end counting. Optional.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    public sealed class GetMeetingsListBindingValidator : AbstractValidator<GetMeetingsListBinding>
    {
        public GetMeetingsListBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}
