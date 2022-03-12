namespace DemoMongoRepository.Settings
{
    /// <summary>
    /// Will hold the values from the 'MongoDbSettings' object in the 'appsettings.json' file
    /// </summary>
    public interface IMongoDbSettings
    {
        /// <summary>
        /// The MongoDB connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// The MongoDB collection name
        /// </summary>
        string DatabaseName { get; set; }
    }
}