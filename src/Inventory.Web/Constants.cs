namespace Athene.Inventory.Web
{
    public static class Constants 
    {
        public const string DateFormat = "yyyy-MM-dd";
        public static class ClaimTypes 
        {
            public const string Permission = "permission";
        }

        public static class Databases
        {
            public const string Sqlite = "sqlite";
            public const string MySql = "mysql";
        }

        public static class Policies 
        {
            public const string Administrator = "Administrator";
            public const string Librarian = "Librarian";
            public const string AdministrateInventory = "AdministrateInventory";
            public const string RentBooks = "RentBooks";
            public const string DataImport = "DataImport";
            public const string DataImportUsers = "DataImport_Users";
            public const string DataImportBooks = "DataImport_Books";
            public const string DataImportInventoryItems = "DataImport_InventoryItems";
        }

        public static class Roles 
        {
            public const string Administrator = "Administrator";
            public const string Librarian = "Librarian";
        }

        public static class Permissions
        {
            public const string AdministrateInventory = "AdministrateInventory";
            public const string RentBooks = "RentBooks";
            public const string DataImport = "DataImport";
            public const string DataImportUsers = "DataImport_Users";
            public const string DataImportBooks = "DataImport_Books";
            public const string DataImportInventoryItems = "DataImport_InventoryItems";
        }
    }
}