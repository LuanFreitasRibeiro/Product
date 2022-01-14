using FluentValidation;
using ProductCatalog.Domain.Request.Brand;

namespace ProductCatalog.Application.Validator.Brand
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandValidator()
        {
            ValidateName();
        }

        private void ValidateName()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("'name' is required");
        }
    }
}
