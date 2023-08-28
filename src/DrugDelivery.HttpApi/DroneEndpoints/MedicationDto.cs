
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugDelivery.HttpApi.DroneEndpoints
{
    public class MedicationDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal Weight { get; set; }
        public string Image { get; set; }
    }
}
