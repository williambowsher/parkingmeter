using System;
using System.Collections.Generic;
using ParkingMeter.ChargeRules;

namespace ParkingMeter
{
    public class ParkingMeter
    {
        public IEnumerable<IChargeRule> ActiveRules { get; set; }

        public double ProcessParkingCharge(DateTime entryTime, DateTime exitTime)
        {
            var charge = 0d;
            var currentPeriod = entryTime;
            while (currentPeriod < exitTime)
            {
                var ruleToApply = RuleThatApplies(currentPeriod);
                if( ruleToApply != null)
                {
                    charge += ruleToApply.PeriodRate;
                    currentPeriod = currentPeriod.Add(ruleToApply.Increment);
                }
                else
                {
                    currentPeriod = currentPeriod.Add(new TimeSpan(0, 59, 59));
                }
            }
            return charge;
        }

        private IChargeRule RuleThatApplies(DateTime currentPeriod)
        {
            foreach (var chargeRule in ActiveRules)
            {
                if (chargeRule.Applies(currentPeriod))
                {
                    return chargeRule;
                }
            }
            return null;
        }
    }
}
