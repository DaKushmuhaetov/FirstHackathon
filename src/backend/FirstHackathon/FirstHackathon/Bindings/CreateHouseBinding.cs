using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class CreateHouseBinding
    {
        /// <summary>
        /// Login for house owner authorization
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password for house owner authorization
        /// </summary>
        public string Password { get; set; }
    }
    public sealed class CreateHouseBindingValidator : AbstractValidator<CreateHouseBinding>
    {
        public CreateHouseBindingValidator()
        {
            RuleFor(b => b.Login)
                .NotEmpty();
            RuleFor(b => b.Password)
                .NotEmpty();
        }
    }
}
