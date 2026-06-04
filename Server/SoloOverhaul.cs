using SPTarkov.Common.Extensions;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Generators;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Enums.Hideout;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SoloOverhaul.Services;
using SPTarkov.Server.Core.Models.Spt;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Common;
using System.Diagnostics;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using SPTarkov.Server.Core.Helpers;

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
        ConfigService configService,
        ProfileHelper profileHelper)
        : IOnLoad
{

    public async Task OnLoad()
    {
        await configService.LoadAsync();

        if (configService.SOHConfig.General.RemoveHideoutTimers == true)
        {
            RemoveHideoutTimers();
        }
        if (configService.SOHConfig.General.RemoveFleaFunctionality == true)
        {
            RemoveFleaFunctionality();
        }
        if (configService.SOHConfig.General.RemoveScavTimer == true)
        {
            RemoveSavageCooldown();
        }
        if (configService.SOHConfig.General.RemoveScavCase == true)
        {
            RemoveScavCase();
        }
        if (configService.SOHConfig.General.RemoveCircleofCultists == true)
        {
            RemoveCircleofCultists();
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

    private void RemoveFleaFunctionality()
    {
        databaseService.GetTables().Globals.Configuration.RagFair.MinUserLevel = 99; // easy but unoptimized i think
    }

    private void RemoveSavageCooldown()
    {
        databaseService.GetTables().Globals.Configuration.SavagePlayCooldown = 0;
    }

    private void RemoveScavCase()
    {
        var hideout = databaseService.GetHideout();
        var hideoutAreas = hideout.Areas;
        var scavcaseArea = hideoutAreas.FirstOrDefault(area => area.Type == HideoutAreas.ScavCase);
        var scavcaseStages = scavcaseArea.Stages;

        foreach (var stageKvP in scavcaseStages)
        {
            var stageRequirements = stageKvP.Value.Requirements;

            foreach (var requiurement in stageRequirements)
            {
                requiurement.RequiredLevel = 99;
            }
        }
    }
    private void RemoveCircleofCultists() // unheard owns circle by default. i can't find a way to remove that rn, just trust the user
    {
        var hideout = databaseService.GetHideout();
        var hideoutAreas = hideout.Areas;
        var circleArea = hideoutAreas.FirstOrDefault(area => area.Type == HideoutAreas.CircleOfCultists);
        var circleStages = circleArea.Stages;

        foreach (var stageKvP in circleStages)
        {
            var stageRequirements = stageKvP.Value.Requirements;

            foreach (var requirement in stageRequirements)
            {
                requirement.RequiredLevel = 99;
            }
        }
    }
}
