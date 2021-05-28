namespace Catalog.API.Database.Helpers.Settings
{
    public interface ICatalogDatabaseSettings
    {
        public string ConnectingString { get; }
        public string CollectionName { get; }
        public string DatabaseName { get; }
    }
}