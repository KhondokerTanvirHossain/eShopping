namespace Catalog.API.Database.Helpers.Settings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ConnectingString { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}