using System.Reflection;
using SoloOverhaul.Models.Config;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Utils;


namespace SoloOverhaul.Services
{
    [Injectable(InjectionType.Singleton)]
    public class ConfigService(ModHelper modHelper, JsonUtil jsonUtil, ISptLogger<ConfigService> logger)
    {
        public SOHConfig SOHConfig { get; private set; } = new();

        public string GetModPath()
        {
            return modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        }

        public string GetConfigPath()
        {
            return Path.Combine(GetModPath(), "Config", "config.json");
        }

        public async Task LoadAsync()
        {
            string configPath = GetConfigPath();
            string configDir = Path.GetDirectoryName(configPath)!;

            SOHConfig loadedConfig = await jsonUtil.DeserializeFromFileAsync<SOHConfig>(configPath);

            if (loadedConfig is not null)
            {
                SOHConfig = loadedConfig;
                logger.Success("[SoloOverhaul] Config loaded successfully!");
            }
            else
            {
                logger.Warning("[SoloOverhaul] No config found, loading default values.");
            }
        }

    }
}
