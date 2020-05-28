using System;
using FluentAssertions;
using ParkingMeter.ChargeRules;
using Xunit;

namespace ParkingMeterTests
{
    public class ChargeRuleTests
    {
        [Fact]
        public void CheckAppliesForSaturdayStartAndEndInRange_ExpectTrue()
        {
            //Arrange
            var longTermRule = new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Saturday }, 
                StartTime = new TimeSpan(0,0,0), 
                EndTime  = new TimeSpan(23,59,59),
                Increment = new TimeSpan(23,59,59),
                PeriodRate = 7.5d
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveRules = new IChargeRule[]{ longTermRule };
            var entryTime = new DateTime(2020,05,23,10,30,0);
            var exitTime = new DateTime(2020,5,23,11,30,0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(longTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartBeforeTimeAndEndInRange_ExpectTrue()
        {
            //Arrange
            var longTermRule = new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Saturday },
                StartTime = new TimeSpan(0, 0, 0),
                EndTime = new TimeSpan(23, 59, 59),
                Increment = new TimeSpan(23, 59, 59),
                PeriodRate = 7.5d
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveRules = new IChargeRule[] { longTermRule };
            var entryTime = new DateTime(2020, 05, 22, 07, 30, 0);
            var exitTime = new DateTime(2020, 5, 23, 08, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(longTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartInRangeAndEndOutOfRange_ExpectTrue()
        {
            //Arrange
            var longTermRule = new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Saturday },
                StartTime = new TimeSpan(0, 0, 0),
                EndTime = new TimeSpan(23, 59, 59),
                Increment = new TimeSpan(23, 59, 59),
                PeriodRate = 7.5d
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveRules = new IChargeRule[] { longTermRule };
            var entryTime = new DateTime(2020, 05, 23, 15, 30, 0);
            var exitTime = new DateTime(2020, 5, 24, 16, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(longTermRule.PeriodRate);
        }

        [Fact]
        public void CheckAppliesForMondayStartOutOfRangeAndEndOutOfRange_ExpectTrue()
        {
            //Arrange
            var longTermRule = new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Saturday },
                StartTime = new TimeSpan(0, 0, 0),
                EndTime = new TimeSpan(23, 59, 59),
                Increment = new TimeSpan(23, 59, 59),
                PeriodRate = 7.5d
            };
            var parkingMeter = new ParkingMeter.ParkingMeter();
            parkingMeter.ActiveRules = new IChargeRule[] { longTermRule };
            var entryTime = new DateTime(2020, 5, 22, 07, 30, 0);
            var exitTime = new DateTime(2020, 5, 24, 16, 30, 0);
            //Act
            var parkingCharge = parkingMeter.ProcessParkingCharge(entryTime, exitTime);

            //Assert
            parkingCharge.Should().Be(longTermRule.PeriodRate);
        }

    }
}
