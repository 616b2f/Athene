namespace Athene.Inventory.Web
{
    public static class Constants 
    {
        public const string DateFormat = "yyyy-MM-dd";
        public static class ClaimTypes 
        {
            public const string Permission = "permission";
        }

        public static class Permissions
        {
            public const string RentBooks = "RentBooks";
            public const string DataImportUsers = "DataImport_Users";
            public const string DataImportBooks = "DataImport_Books";
            public const string DataImportInventoryItems = "DataImport_InventoryItems";
        }
    }
}