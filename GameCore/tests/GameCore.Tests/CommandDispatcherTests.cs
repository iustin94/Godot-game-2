using GameCore.Commands;

namespace GameCore.Tests;

public class CommandDispatcherTests
{
    private class TestContext : ICommandContext { }

    private class SuccessCommand : ICommand
    {
        public bool CanExecute(ICommandContext context) => true;
        public CommandResult Execute(ICommandContext context) => CommandResult.Ok();
    }

    private class FailCommand : ICommand
    {
        public bool CanExecute(ICommandContext context) => true;
        public CommandResult Execute(ICommandContext context) => CommandResult.Fail("boom");
    }

    private class CannotExecuteCommand : ICommand
    {
        public bool CanExecute(ICommandContext context) => false;
        public CommandResult Execute(ICommandContext context) => CommandResult.Ok();
    }

    [Fact]
    public void Successful_command_is_added_to_history()
    {
        var dispatcher = new CommandDispatcher();
        var cmd = new SuccessCommand();

        dispatcher.Dispatch(cmd, new TestContext());

        Assert.Single(dispatcher.History);
        Assert.Same(cmd, dispatcher.History[0]);
    }

    [Fact]
    public void Failed_command_is_not_added_to_history()
    {
        var dispatcher = new CommandDispatcher();

        dispatcher.Dispatch(new FailCommand(), new TestContext());

        Assert.Empty(dispatcher.History);
    }

    [Fact]
    public void CanExecute_false_returns_failure()
    {
        var dispatcher = new CommandDispatcher();

        var result = dispatcher.Dispatch(new CannotExecuteCommand(), new TestContext());

        Assert.False(result.Success);
        Assert.Empty(dispatcher.History);
    }
}
