using FluentValidation;
using System;

namespace FirstHackathon.Bindings
{
    public sealed class CreateMeetingBinding
    {
        /// <summary>
        /// Название собрания
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Дата проведения собрания (utc)
        /// </summary>
        public DateTime MeetingDate { get; set; }

        /// <summary>
        /// Описание собрания
        /// </summary>
        public string Description { get; set; }
    }

    public sealed class CreateMeetingBindingValidator : AbstractValidator<CreateMeetingBinding>
    {
        public CreateMeetingBindingValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleFor(b => b.MeetingDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(b => b.Description)
                .MaximumLength(1000);
        }
    }
}
