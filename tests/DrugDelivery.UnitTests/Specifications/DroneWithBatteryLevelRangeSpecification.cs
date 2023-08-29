
using DrugDelivery.Core.Entities;
using Moq;
using Xunit;

namespace DrugDelivery.UnitTests.Core.Specifications
{
    public class DroneWithBatteryLevelRangeSpecification
    {

        [Theory]
        [InlineData(0, 25, 1)]
        [InlineData(25, 100, 2)]
        [InlineData(50, 100, 1)]
        [InlineData(0, 10, 0)]
        public void MatchesExpectedNumberOfDrones(decimal min, decimal max, int expectedCount)
        {
            var spec = new DrugDelivery.Core.Specifications.DroneWithBatteryLevelRangeSpecification(min, max);

            var result = spec.Evaluate(GetTestDroneCollection()).ToList();

            Assert.Equal(expectedCount, result.Count);
        }
        public static List<Drone> GetTestDroneCollection()
        {
            var drone1Mock = new Mock<Drone>("TF-64767", DroneModel.LIGHTWEIGHT, 50M, 100M, DroneState.IDLE);
            var drone2Mock = new Mock<Drone>("AB-34567", DroneModel.MIDDLEWEIGHT, 100M, 80M, DroneState.LOADED);
            var drone3Mock = new Mock<Drone>("DB-45577", DroneModel.HEAVYWEIGHT, 200M, 24M, DroneState.IDLE);
            var drone4Mock = new Mock<Drone>("FB-04567", DroneModel.HEAVYWEIGHT, 200M, 30M, DroneState.IDLE);

            return new List<Drone>()
            {
                drone1Mock.Object,
                drone2Mock.Object,
                drone3Mock.Object,
                drone4Mock.Object
            };
        }
    }
}
