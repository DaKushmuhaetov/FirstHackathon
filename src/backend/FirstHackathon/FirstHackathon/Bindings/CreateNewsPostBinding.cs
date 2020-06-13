using FluentValidation;

namespace FirstHackathon.Bindings
{
    public sealed class CreateNewsPostBinding
    {
        /// <summary>
        /// Название собрания
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание собрания
        /// </summary>
        public string Description { get; set; }
    }

    public sealed class CreateNewsPostBindingValidator : AbstractValidator<CreateNewsPostBinding>
    {
        public CreateNewsPostBindingValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleFor(b => b.Description)
                .NotEmpty();
        }
    }
}
