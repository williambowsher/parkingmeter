using System;
using System.Collections.Generic;
using System.Linq;
using ParkingMeter.ChargeRules;

namespace ParkingMeter
{
    public class ParkingMeter
    {
        public IEnumerable<IChargeRule> ActiveSchemes { get; set; }

        public decimal ProcessParkingCharge(ChargingScheme customerSelection, DateTime entryTime, DateTime exitTime)
        {
            var scheme = ActiveSchemes.Single(rule => rule.Scheme == customerSelection);
            return scheme.Calculate(customerSelection, entryTime, exitTime);
        }
    }
}
