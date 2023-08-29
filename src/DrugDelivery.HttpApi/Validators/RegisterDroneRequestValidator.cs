using Drugdelivery.HttpApi.DroneEndpoints;
using DrugDelivery.Core.Entities;
using FluentValidation;

namespace DrugDelivery.HttpApi.Validators
{
    public class RegisterDroneRequestValidator : AbstractValidator<RegisterDroneRequest>
    {
        public RegisterDroneRequestValidator() 
        {
            RuleFor(e => e.SerialNumber)
                .NotEmpty()
                .WithErrorCode("1041")
                .MaximumLength(100)
                .WithErrorCode("1042");

            RuleFor(e => e.Model)
                .NotEmpty()
                .WithErrorCode("1051")
                .IsEnumName(typeof(DroneModel), caseSensitive: false)
                .WithErrorCode("1052");

            RuleFor(e => e.WeightLimit)
                .NotNull()
                .WithErrorCode("1061")
                .InclusiveBetween(0, 500)
                .WithErrorCode("1062");

            RuleFor(e => e.BatteryCapacity)
                .NotNull()
                .WithErrorCode("1071")
                .InclusiveBetween(0, 100)
                .WithErrorCode("1072");
        }
    }
}
