using System;

namespace ParkingMeter.ChargeRules
{
    public class LongStayRule : ChargeRule
    {
        public override decimal Calculate(ChargingScheme customerSelection, DateTime entryTime, DateTime exitTime)
        {
            var days = (exitTime.Day-entryTime.Day)+1;
            return days * this.PeriodRate;
        }
    }
}
