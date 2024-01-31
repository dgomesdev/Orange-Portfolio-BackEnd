using FluentValidation;
using Orange_Portfolio_BackEnd.Application.ViewModel;

namespace Orange_Portfolio_BackEnd.Application.Validators
{
    public class CreateProjectValidator : AbstractValidator<ProjectViewModel>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(45)
                .WithMessage("Title supports a maximum of 45 characters");

            RuleFor(p => p.Link)
                .NotEmpty()
                .WithMessage("Link is required")
                .MaximumLength(255)
                .WithMessage("Link supports a maximum of 255 characters");

            RuleFor(p => p.Image)
                .NotEmpty()
                .WithMessage("Image is required")
                .MaximumLength(255)
                .WithMessage("Image supports a maximum of 255 characters");

            RuleFor(x => x.Tags)
                .Must(tags => tags == null || tags.All(tag => !string.IsNullOrWhiteSpace(tag.Name)))
                .WithMessage("Tag name cannot be empty");
        }
    }
}
