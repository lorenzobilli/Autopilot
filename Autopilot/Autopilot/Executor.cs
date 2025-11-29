using Autopilot.Model;

namespace Autopilot;

public class Executor(DirectiveHandler directiveHandler)
{
    private readonly DirectiveHandler _directiveHandler = directiveHandler;

    public ExecutionMode Mode { get; set; }

    public void Execute()
    {
        foreach (var directive in _directiveHandler.Directives)
        {
            if (Mode == ExecutionMode.Retrieve)
            {
                ExecuteRetrieveDirective(directive);
            }
            else
            {
                ExecuteDeployDirective(directive);
            }
        }
    }

    private void ExecuteRetrieveDirective(Directive directive)
    {

    }

    private void ExecuteDeployDirective(Directive directive)
    {

    }
}
