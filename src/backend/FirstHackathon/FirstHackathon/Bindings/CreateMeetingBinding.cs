using FluentValidation;
using System;

namespace FirstHackathon.Bindings
{
    public sealed class CreateMeetingBinding
    {
        public string Title { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Description { get; set; }
    }

    public sealed class CreateMeetingBindingValidator : AbstractValidator<CreateMeetingBinding>
    {
        public CreateMeetingBindingValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleFor(b => b.MeetingDate)
                .NotEmpty();
            RuleFor(b => b.Description)
                .MaximumLength(1000);
        }
    }
}
