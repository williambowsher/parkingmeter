using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingMeter.ChargeRules
{
    public class ChargeRule : IChargeRule
    {
        public double PeriodRate { get; set; }

        public TimeSpan Increment { get; set; }
        public IEnumerable<DayOfWeek> ActiveDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool Applies(DateTime currentPeriod)
        {
            var dayOfWeek = currentPeriod.DayOfWeek;

            if (ActiveDays.Contains(dayOfWeek))
            {
                var startTimeCheck = new DateTime(currentPeriod.Year, currentPeriod.Month, currentPeriod.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);
                var endTimeCheck = new DateTime(currentPeriod.Year, currentPeriod.Month, currentPeriod.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds);

                if (currentPeriod >= startTimeCheck && currentPeriod <= endTimeCheck)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
