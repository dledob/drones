
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugDelivery.HttpApi.DroneEndpoints
{
    public class MedicationDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Weight { get; set; }
        public string? Image { get; set; }
    }
}
