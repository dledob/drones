using Drugdelivery.HttpApi.DroneEndpoints;
using DrugDelivery.HttpApi.DroneEndpoints;
using FluentValidation;

namespace DrugDelivery.HttpApi.Validators
{
    public class LoadingDroneRequestValidator : AbstractValidator<LoadingDroneRequest>
    {
        public LoadingDroneRequestValidator()
        {
            RuleFor(e => e.Medications)
                .NotNull()
                .WithErrorCode("108");
            RuleForEach(e => e.Medications)
                .ChildRules(medication =>
                {
                    medication.RuleFor(e => e.Name)
                        .NotEmpty()
                        .WithErrorCode("1081")
                        .Matches("^[a-zA-Z0-9_-]*$")
                        .WithErrorCode("1082")
                        .WithMessage("'{PropertyValue}' is not in the correct format.");

                    medication.RuleFor(e => e.Code)
                        .NotEmpty()
                        .WithErrorCode("1083")
                        .Matches("^[A-Z0-9_]*$")
                        .WithErrorCode("1084")
                        .WithMessage("'{PropertyValue}' is not in the correct format.");

                    medication.RuleFor(e => e.Weight)
                        .NotNull()
                        .WithErrorCode("1085")
                        .GreaterThan(0)
                        .WithErrorCode("1086");
                });
        }
    }
}
