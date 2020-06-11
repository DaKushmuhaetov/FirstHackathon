using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class AuthenticationBinding
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public sealed class AuthenticationBindingValidator : AbstractValidator<AuthenticationBinding>
    {
        public AuthenticationBindingValidator()
        {
            RuleFor(b => b.Login)
                   .NotEmpty();
            RuleFor(b => b.Password)
                 .NotEmpty();
        }
    }
}
