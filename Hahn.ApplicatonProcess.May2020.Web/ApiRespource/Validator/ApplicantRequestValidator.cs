using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Domain.IService;
using Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Request;

namespace Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Validator
{
    public class ApplicantRequestValidator : AbstractValidator<ApplicantRequest>
    {
        public ApplicantRequestValidator(ICountryService countrySevice)
        {
            RuleFor(x => x.name).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.familyName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.address).NotEmpty().NotNull().MinimumLength(10);
            RuleFor(x => x.countryOfOrigin).NotEmpty().NotNull().Must(x => countrySevice.ValidateCountry(x).Result == true).WithMessage("invalid country input");
            RuleFor(x => x.emailAddress).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
        }
    }
}