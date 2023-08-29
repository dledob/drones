using AutoMapper;
using DrugDelivery.Core.Entities;
using DrugDelivery.HttpApi.DroneEndpoints;

namespace DrugDelivery.HttpApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Drone, DroneDto>()
            .ReverseMap();
        CreateMap<Medication, MedicationDto>()
            .ReverseMap();
    }
}
