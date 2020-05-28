using System.Linq;
using FluentAssertions;
using ParkingMeter.ChargeRules;
using Xunit;

namespace ParkingMeterTests
{
    public class ParkingMeterTests
    {
        [Fact]
        public void CreateParkingMeter_Expect2RulesActive()
        {
            //Arrange
            var parkingMeter = new ParkingMeter.ParkingMeter();
            var shortTermRule = new ChargeRule();
            var longTermRule = new ChargeRule();
            //Act
            parkingMeter.ActiveRules = new IChargeRule[] { shortTermRule, longTermRule };
            //Assert
            parkingMeter.ActiveRules.Count().Should().Be(2);
        }
    }
}
