using System;

namespace ParkingMeter.ChargeRules
{
    public class ShortStayRule : ChargeRule
    {
        public override decimal Calculate(ChargingScheme customerSelection, DateTime entryTime, DateTime exitTime)
        {
            var charge = 0m;
            var currentPeriod = entryTime;
            while (currentPeriod < exitTime)
            {
                if (this.Applies(currentPeriod))
                {
                    charge += this.PeriodRate;
                    currentPeriod = currentPeriod.Add(this.Increment);
                }
                else
                {
                    currentPeriod = currentPeriod.Add(this.Increment.Subtract(new TimeSpan(0, 0, 1)));
                }
            }
            return charge;
        }
    }
}
