using System;

namespace ModernCsharp
{
    public static class TollCalculations
    {
        private static bool IsWeekDay(DateTime timeOfToll) =>
            timeOfToll.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,
                _ => true
            };

        private enum TimeBand
        {
            MorningRush,
            Daytime,
            EveningRush,
            Overnight
        }

        private static TimeBand GetTimeBand(DateTime timeOfToll)
        {
            int hour = timeOfToll.Hour;
            if (hour < 6)
                return TimeBand.Overnight;
            else if (hour < 10)
                return TimeBand.MorningRush;
            else if (hour < 16)
                return TimeBand.Daytime;
            else if (hour < 20)
                return TimeBand.EveningRush;
            else
                return TimeBand.Overnight;
        }

        // business requirements
        // Calculate a peak time multiplier:
        // weekend multiplier is 1.0
        // late nigh / early morning is a discount, 0.75
        // daytime during any weekday is 1.5
        // morning rush inbound is double (2.0)
        // evening rush inbound is 1.0
        // evening outbound is double (2.0)
        public static decimal PeakTimePremiumImperative(DateTime timeOfToll, bool inbound)
        {
            if (IsWeekDay(timeOfToll))
            {
                if (inbound)
                {
                    var timeBand = GetTimeBand(timeOfToll);
                    if (timeBand == TimeBand.MorningRush)
                    {
                        return 2.00m;
                    }
                    else if (timeBand == TimeBand.Daytime)
                    {
                        return 1.50m;
                    }
                    else if (timeBand == TimeBand.EveningRush)
                    {
                        return 1.00m;
                    }
                    else
                    {
                        return 0.75m;
                    }
                }
                else
                {
                    var timeBand = GetTimeBand(timeOfToll);
                    if (timeBand == TimeBand.MorningRush)
                    {
                        return 1.00m;
                    }
                    else if (timeBand == TimeBand.Daytime)
                    {
                        return 1.50m;
                    }
                    else if (timeBand == TimeBand.EveningRush)
                    {
                        return 2.00m;
                    }
                    else
                    {
                        return 0.75m;
                    }
                }
            }
            else
            {
                return 1.00m;
            }
        }

        // business requirements
        // Calculate a peak time multiplier:
        // weekend multiplier is 1.0
        // late nigh / early morning is a discount, 0.75
        // daytime during any weekday is 1.5
        // morning rush inbound is double (2.0)
        // evening rush inbound is 1.0
        // evening outbound is double (2.0)
        public static decimal PeakTimePremium(DateTime timeOfToll, bool inbound) =>
            (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.MorningRush, true) => 2.00m,
                (true, TimeBand.MorningRush, false) => 1.00m,
                (true, TimeBand.Daytime, _) => 1.50m,
                (true, TimeBand.EveningRush, true) => 1.00m,
                (true, TimeBand.EveningRush, false) => 2.00m,
                (true, TimeBand.Overnight, _) => 0.75m,
                (_, _, _) => 1.00m
            };

        public static (DateTime start, DateTime end) GenerateSubscription()
        {
            var start = DateTime.Now.Date;
            var end = start.AddYears(1);
            return (start, end);
        }
    }
}
