
using DrugDelivery.Core.Entities;
using Moq;
using Xunit;

namespace DrugDelivery.UnitTests.Core.Specifications
{
    public class DronesAvailableForLoadingSpecification
    {
        [Fact]
        public void MatchesAvailableForLoading()
        {
            var spec = new DrugDelivery.Core.Specifications.DronesAvailableForLoadingSpecification();

            var result = spec.Evaluate(GetTestDroneCollection());

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("TF-64767", result.FirstOrDefault()?.SerialNumber);
        }
        public static List<Drone> GetTestDroneCollection()
        {
            var drone1Mock = new Mock<Drone>("TF-64767", DroneModel.LIGHTWEIGHT, 50M, 100M, DroneState.IDLE);
            var drone2Mock = new Mock<Drone>("AB-34567", DroneModel.MIDDLEWEIGHT, 100M, 80M, DroneState.LOADED);
            var drone3Mock = new Mock<Drone>("DB-45577", DroneModel.HEAVYWEIGHT, 200M, 24M, DroneState.IDLE);

            return new List<Drone>()
            {
                drone1Mock.Object,
                drone2Mock.Object,
                drone3Mock.Object
            };
        }
    }
}
