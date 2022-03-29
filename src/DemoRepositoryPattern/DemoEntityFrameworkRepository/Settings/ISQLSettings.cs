namespace DemoEntityFrameworkRepository.Settings;

/// <summary>
/// Will hold the values from the 'ISQLSettings' object in the 'appsettings.json' file
/// </summary>
public interface ISQLSettings
{
    string ConnectionString { get; set; }
}
