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
            .Must(BeAValidImage)
            .WithMessage("Image must be a JPEG or PNG file");

            RuleFor(x => x.Tags)
                .Must(tags => tags == null || tags.All(tag => !string.IsNullOrWhiteSpace(tag)))
                .WithMessage("Tag name cannot be empty");
        }

        private bool BeAValidImage(IFormFile image)
        {
            if (image == null)
                return true; // No image provided, validation should pass

            var allowedFormats = new List<string> { "image/jpeg", "image/png" };
            return allowedFormats.Contains(image.ContentType);
        }
    }
}
