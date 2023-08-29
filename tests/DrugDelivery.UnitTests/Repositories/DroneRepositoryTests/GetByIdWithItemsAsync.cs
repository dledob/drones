using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Specifications;
using DrugDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DrugDelivery.UnitTests.Repositories.DroneRepositoryTests;

public class GetByIdWithMedicationsAsync
{
    private readonly DrugDeliveryDbContext _context;
    private readonly EfRepository<Drone> _droneRepository;

    public GetByIdWithMedicationsAsync()
    {
        var dbOptions = new DbContextOptionsBuilder<DrugDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDrugDelivery")
            .Options;
        _context = new DrugDeliveryDbContext(dbOptions);
        _droneRepository = new EfRepository<Drone>(_context);
    }

    [Fact]
    public async Task GetDroneAndMedicationsLoaded()
    {
        //Arrange
        var droneId = Guid.NewGuid();

        var drone = _context.Drones.Add(new Drone(droneId, "AB-34567", DroneModel.MIDDLEWEIGHT, 100M, 80M, DroneState.LOADED));
        

        var medications = new List<Medication>() {
            new Medication("Ampicillin", "AMPN", 500),
            new Medication("Annovera", "ANN", 25),
            new Medication("Atenolol ", "ATN", 50)
        };
        _context.Medications.AddRange(medications);

        _context.LoadedMedications.AddRange(medications.Select(m => new LoadedMedication(droneId, m.Id)));

        _context.SaveChanges();

        //Act
        var spec = new DroneWithMedicationsSpecification(droneId);
        var droneFromRepo = await _droneRepository.FirstOrDefaultAsync(spec);

        //Assert
        Assert.NotNull(droneFromRepo);
        Assert.NotNull(droneFromRepo.LoadedMedications);
        Assert.Equal(DroneState.LOADED, droneFromRepo.State);
        Assert.Equal(3, droneFromRepo.LoadedMedications.Count);        
    }
}
