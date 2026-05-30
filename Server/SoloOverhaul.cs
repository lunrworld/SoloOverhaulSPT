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

namespace SoloOverhaul;


public record ModMetadata : AbstractModMetadata
{

    public override string ModGuid { get; init; } = "com.lunarworld.solooverhaul";
    public override string Name { get; init; } = "SoloOverhaul";
    public override string Author { get; init; } = "LunarWorld";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; }
    public override string License { get; init; } = "MIT";
}
    [Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
    public class EditDatabaseValues(
        ISptLogger<EditDatabaseValues> logger, 
        DatabaseService databaseService)
        : IOnLoad 
{

    public Task OnLoad()
    {
        RemoveHideoutTimers();

        DisableFlea();

        RebalanceSecureContainers();

        logger.Success("SOH Loaded!");

        return Task.CompletedTask;
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

    private void DisableFlea()
    {
        databaseService.GetGlobals().Configuration.RagFair.Enabled = false;
    }   

    private void RebalanceSecureContainers()
    {
        var gamma = databaseService.GetTables().Templates.Items["5857a8bc2459772bad15db29"];
        gamma.Properties.Grids.ToList()[0].Properties.Filters.ToList()[0].ExcludedFilter.Add("5485a8684bdc2da71d8b4567");
    }
}
