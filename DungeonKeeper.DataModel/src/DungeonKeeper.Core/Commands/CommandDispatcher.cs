namespace DungeonKeeper.Core.Commands;

public class CommandDispatcher
{
    private readonly List<ICommand> _history = new();

    public CommandResult Dispatch(ICommand command, ICommandContext context)
    {
        if (!command.CanExecute(context))
            return CommandResult.Fail("Command preconditions not met");

        var result = command.Execute(context);
        if (result.Success)
            _history.Add(command);

        return result;
    }

    public IReadOnlyList<ICommand> History => _history;
}
