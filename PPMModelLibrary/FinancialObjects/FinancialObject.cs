namespace PPMAPIModelLibrary.FinancialObjects
{
    public class FinancialObject
    {
        public const string Cost = "Cost";
        public const string Revenue = "Revenue";
        public const string ValueIncrease = "ValueIncrease";
        public const string ValueDecrease = "ValueDecrease";

        public static readonly string[] Types = new string[] { Cost, Revenue, ValueIncrease, ValueDecrease };
    }
}
