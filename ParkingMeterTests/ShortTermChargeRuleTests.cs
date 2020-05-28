using System;
using FluentAssertions;
using ParkingMeter;
using ParkingMeter.ChargeRules;
using Xunit;

namespace ParkingMeterTests
{
    public class ShortTermChargeRuleTests
    {
        private ChargingScheme CustomerSelection = ChargingScheme.ShortStay;

        [Fact]
        public void CheckAppliesForMondayStartAndEndInRange_ExpectTrue()
        {
            //Arrange
            var shortTermRule = new ShortStayRule
            {
                ActiveDays = new[] { DayOfWeek.Monday }, 
                StartTime = new TimeSpan(8,0,0), 
                EndTime  = new TimeSpan(18,0,0),
                Increment = new TimeSpan(1,0,0),
                Scheme = ChargingScheme.ShortStay,
                PeriodRate = 1.1m
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveSchemes = new IChargeRule[]{ shortTermRule };
            var entryTime = new DateTime(2020,05,25,10,30,0);
            var exitTime = new DateTime(2020,5,25,11,30,0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(CustomerSelection,entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(shortTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartBeforeTimeAndEndInRange_ExpectTrue()
        {
            //Arrange
            var shortTermRule = new ShortStayRule
            {
                ActiveDays = new[] { DayOfWeek.Monday },
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(18, 0, 0),
                Increment = new TimeSpan(1, 0, 0),
                Scheme = ChargingScheme.ShortStay,
                PeriodRate = 1.1m
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveSchemes = new IChargeRule[] { shortTermRule };
            var entryTime = new DateTime(2020, 05, 25, 07, 30, 0);
            var exitTime = new DateTime(2020, 5, 25, 08, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(CustomerSelection, entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(shortTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartInRangeAndEndOutOfRange_ExpectTrue()
        {
            //Arrange
            var shortTermRule = new ShortStayRule
            {
                ActiveDays = new[] { DayOfWeek.Monday },
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(18, 0, 0),
                Increment = new TimeSpan(1, 0, 0),
                Scheme = ChargingScheme.ShortStay,
                PeriodRate = 1.1m
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveSchemes = new IChargeRule[] { shortTermRule };
            var entryTime = new DateTime(2020, 05, 25, 15, 30, 0);
            var exitTime = new DateTime(2020, 5, 25, 16, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(CustomerSelection, entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(shortTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartOutOfRangeAndEndOutOfRange_ExpectTrue()
        {
            //Arrange
            var shortTermRule = new ShortStayRule
            {
                ActiveDays = new[] { DayOfWeek.Monday },
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(18, 0, 0),
                Increment = new TimeSpan(1, 0, 0),
                Scheme = ChargingScheme.ShortStay,
                PeriodRate = 1.1m
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveSchemes = new IChargeRule[] { shortTermRule };
            var entryTime = new DateTime(2020, 05, 25, 07, 30, 0);
            var exitTime = new DateTime(2020, 5, 25, 18, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(CustomerSelection, entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(11m);
        }
    }
}
