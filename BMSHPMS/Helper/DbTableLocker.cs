namespace BMSHPMS.Helper
{
    public static class DbTableLocker
    {
        public static object DSDonationTransactionEvent => new object();

        public static object T_Receipt => new object();

        public static object T_Receipt_del => new object();

        public static object T_Donor => new object();

        public static object T_Longevity => new object();

        public static object T_Memorial => new object();

        public static object T_Opt_DonationProject => new object();
    }
}
