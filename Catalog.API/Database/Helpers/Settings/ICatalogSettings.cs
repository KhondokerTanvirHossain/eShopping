namespace Catalog.API.Database.Helpers.Settings
{
    public interface ICatalogDatabaseSettings
    {
        public string ConnectingString { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}