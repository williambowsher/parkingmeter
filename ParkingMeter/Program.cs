using System;
using ParkingMeter.ChargeRules;

namespace ParkingMeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var parkingMeter = new ParkingMeter();
            parkingMeter.ActiveRules = new IChargeRule[]{ ShortTermRule(), LongTermRule() };

            var exampleAEntryTime = new DateTime(2017, 9, 7, 18, 50, 0);
            var exampleAExitTime = new DateTime(2017, 9, 7, 20, 50, 0);
            var exampleAResult = parkingMeter.ProcessParkingCharge(exampleAEntryTime, exampleAExitTime);
            Console.WriteLine($"Example A: {exampleAEntryTime} - {exampleAExitTime} incurred a charge of {exampleAResult:C2}");

            var exampleBEntryTime = new DateTime(2017,9,7,16,50,0); // 07/09/2017 16:50:00
            var exampleBExitTime = new DateTime(2017,9,9,19,15,0); // 09/09/2017 19:15:00
            var exampleBResult = parkingMeter.ProcessParkingCharge(exampleBEntryTime, exampleBExitTime);
            Console.WriteLine($"Example B: {exampleBEntryTime} - {exampleBExitTime} incurred a charge of {exampleBResult:C2}");

            var exampleCEntryTime = new DateTime(2017,9,7,7,50,0);// 07/09/2017 07:50:00
            var exampleCExitTime = new DateTime(2017,9,9,5,20,0); // 09/09/2017 05:20:00
            var exampleCResult = parkingMeter.ProcessParkingCharge(exampleCEntryTime, exampleCExitTime);
            Console.WriteLine($"Example C: {exampleCEntryTime} - {exampleCExitTime} incurred a charge of {exampleCResult:C2}");
        }
        
        private static IChargeRule LongTermRule()
        {
            return new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
                StartTime = new TimeSpan(0, 0, 0),
                EndTime = new TimeSpan(23, 59, 59),
                Increment = new TimeSpan(23, 59, 59),
                PeriodRate = 7.5d
            };
        }
        private static IChargeRule ShortTermRule()
        {
            return new ChargeRule
            {
                ActiveDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Increment = new TimeSpan(1, 0, 0),
                PeriodRate = 1.1d
            };
        }
    }
}
