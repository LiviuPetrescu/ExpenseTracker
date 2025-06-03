namespace ExpenseTracker.Helpers
{
    public static class ExpenseCategories
    {
        public const string Food = "Food";
        public const string Transportation = "Transportation";
        public const string Utilities = "Utilities";
        public const string Entertainment = "Entertainment";
        public const string Healthcare = "Healthcare";
        public const string Education = "Education";
        public const string Housing = "Housing";
        public const string Travel = "Travel";
        public const string Insurance = "Insurance";
        public const string Shopping = "Shopping";
        public const string Other = "Other";

        public static List<string> All => new()
        {
            Food,
            Transportation,
            Utilities,
            Entertainment,
            Healthcare,
            Education,
            Housing,
            Travel,
            Insurance,
            Shopping,
            Other
        };
    }
}
