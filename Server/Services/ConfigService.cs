using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Utils;


namespace SoloOverhaul.Services
{
    [Injectable(InjectionType.Singleton)]
    internal class ConfigService(ModHelper modHelper, JsonUtil jsonUtil, ISptLogger<ConfigService> logger)
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

    }
}
