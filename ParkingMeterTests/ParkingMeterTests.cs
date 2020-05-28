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
            var shortTermRule = new ShortStayRule();
            var longTermRule = new LongStayRule();
            //Act
            parkingMeter.ActiveSchemes = new IChargeRule[] { shortTermRule, longTermRule };
            //Assert
            parkingMeter.ActiveSchemes.Count().Should().Be(2);
        }
    }
}
