using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class CreatePersonBinding
    {
        /// <summary>
        /// Person name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Person surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Login for person authorization
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password for person authorization
        /// </summary>
        public string Password { get; set; }
    }
    public sealed class CreatePersonBindingValidator : AbstractValidator<CreatePersonBinding>
    {
        public CreatePersonBindingValidator()
        {
            RuleFor(b => b.Name)
                   .NotEmpty();
            RuleFor(b => b.Surname)
                 .NotEmpty();
            RuleFor(b => b.Login)
                .NotEmpty();
            RuleFor(b => b.Password)
                .NotEmpty();
        }
    }
}
