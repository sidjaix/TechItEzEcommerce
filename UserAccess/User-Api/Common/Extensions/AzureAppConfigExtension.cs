using Azure.Data.AppConfiguration;

namespace User_Api.Common.Extensions;

public class AzureAppConfigExtension
{
    private readonly IConfiguration configuration;
    public AzureAppConfigExtension(IConfiguration config)
    {
        configuration = config;
    }
    public async void SetAzureAppConfig()
    {
        var connectionString = configuration.GetConnectionString("AppConfig");
        var client = new ConfigurationClient(connectionString);

        string azureDbKey = "ConnectionStrings:AzureDB";
        string azureDbValue = string.Empty;

        ConfigurationSetting setting = new ConfigurationSetting(azureDbKey, azureDbValue);
        await client.SetConfigurationSettingAsync(setting);

        //Console.WriteLine($"Key '{azureDbKey}' with value '{azureDbValue}' added to App Configuration.");
    }
}
