using System;
using System.Collections.Generic;

namespace ParkingMeter.ChargeRules
{
    public interface IChargeRule
    {
        public double PeriodRate { get; set; }
        public TimeSpan Increment { get; set; }
        public IEnumerable<DayOfWeek> ActiveDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Applies(DateTime currentPeriod);
    }
}
