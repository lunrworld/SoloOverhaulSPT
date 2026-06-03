using SPTarkov.Common.Extensions;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Extensions;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Enums.Hideout;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SoloOverhaul.Services;
using SoloOverhaul.Models.Config;

namespace SoloOverhaul;
public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.lunarworld.solooverhaul";
    public override string Name { get; init; } = "SoloOverhaul";
    public override string Author { get; init; } = "LunarWorld";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.13");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; }
    public override string License { get; init; } = "CC BY-SA 4.0";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
    public class EditDatabaseValues(
        ISptLogger<EditDatabaseValues> logger, 
        DatabaseService databaseService,
        ConfigService configService)
        : IOnLoad 
{

    public async Task OnLoad()
    {
        await configService.LoadAsync();

        if (configService.SOHConfig.General.RemoveHideoutTimers == true)
        {
            RemoveHideoutTimers();
        }

        logger.Success("SOH Loaded!");
    }

    private void RemoveHideoutTimers()
    {
        var hideout = databaseService.GetHideout();
        var hideoutAreas = hideout.Areas;
        var hideoutProduction = hideout.Production;

        foreach (var area in hideoutAreas)
        {
            foreach (var stageKvP in area.Stages)
            {
                stageKvP.Value.ConstructionTime = 0; // For some reason this automatically skips the install button.
            }
        }
        foreach (var recipe in hideoutProduction.Recipes)
        {
            recipe.ProductionTime = 0;
        }

    }
}
