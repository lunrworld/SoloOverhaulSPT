using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Controllers;
using SPTarkov.Reflection.Patching;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ContractsController;

[Injectable(TypePriority = OnLoadOrder.PreSptModLoader)]
public class ContractsController(
    ISptLogger<ContractsController> logger) : IOnLoad
{
    public Task OnLoad()
    {
        new TriggerContract().Enable();

        return Task.CompletedTask;
    }
}

public class TriggerContract : AbstractPatch
{
    protected override MethodBase GetTargetMethod()
    {
        throw new NotImplementedException();
    }
}
