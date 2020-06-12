using FluentValidation;
using System.Collections.Generic;

namespace FirstHackathon.Bindings
{
    public sealed class CreateVotingBinding
    {
        public string Title { get; set; }
        public List<string> Variants { get; set; }
    }

    public sealed class CreateVotingBindingValidator : AbstractValidator<CreateVotingBinding>
    {
        public CreateVotingBindingValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleForEach(b => b.Variants)
                .NotEmpty();
        }
    }
}
