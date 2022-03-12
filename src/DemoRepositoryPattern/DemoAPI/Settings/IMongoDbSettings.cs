namespace DemoAPI.Settings
{
    /// <summary>
    /// Will hold the values from the 'MongoDbSettings' object in the 'appsettings.json' file
    /// </summary>
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}