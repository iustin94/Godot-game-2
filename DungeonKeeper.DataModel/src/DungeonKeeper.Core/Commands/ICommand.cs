namespace DungeonKeeper.Core.Commands;

public interface ICommand
{
    bool CanExecute(ICommandContext context);
    CommandResult Execute(ICommandContext context);
}

public interface ICommand<TResult>
{
    bool CanExecute(ICommandContext context);
    TResult Execute(ICommandContext context);
}

public record CommandResult(bool Success, string? ErrorMessage = null)
{
    public static CommandResult Ok() => new(true);
    public static CommandResult Fail(string error) => new(false, error);
}

public interface ICommandContext { }
