

using DrugDelivery.Core.Entities;
using Moq;
using Xunit;

namespace DrugDelivery.UnitTests.Core.Specifications
{
    public class DroneBySerialNumberSpecification
    {
        private readonly string _serialNumber = "AB-34567";

        [Fact]
        public void MatchesDroneWithGivenSerialNumber()
        {
            var spec = new DrugDelivery.Core.Specifications.DroneBySerialNumberSpecification(_serialNumber);

            var result = spec.Evaluate(GetTestDroneCollection()).FirstOrDefault();

            Assert.NotNull(result);
        }

        [Fact]
        public void MatchesNoDroneBySerialNumber()
        {
            string serial = "serial";
            var spec = new DrugDelivery.Core.Specifications.DroneBySerialNumberSpecification(serial);

            var result = spec.Evaluate(GetTestDroneCollection()).Any();

            Assert.False(result);
        }

        public static List<Drone> GetTestDroneCollection()
        {
            var drone1Mock = new Mock<Drone>("TF-64767", DroneModel.LIGHTWEIGHT, 50M, 100M, DroneState.IDLE);
            var drone2Mock = new Mock<Drone>("AB-34567", DroneModel.MIDDLEWEIGHT, 100M, 80M, DroneState.IDLE);
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
