using GameCore.Common;
using GameCore.Events;

namespace GameCore.Tests;

public class EventBusTests
{
    private record TestEvent(GameTime Timestamp) : IGameEvent
    {
        public TestEvent() : this(new GameTime(0, 0f, 0f)) { }
    }

    [Fact]
    public void Can_publish_and_receive_events()
    {
        var bus = new EventBus();
        TestEvent? received = null;
        bus.Subscribe<TestEvent>(e => received = e);

        var evt = new TestEvent();
        bus.Publish(evt);

        Assert.Same(evt, received);
    }

    [Fact]
    public void Multiple_subscribers_receive_the_same_event()
    {
        var bus = new EventBus();
        int callCount = 0;
        bus.Subscribe<TestEvent>(_ => callCount++);
        bus.Subscribe<TestEvent>(_ => callCount++);

        bus.Publish(new TestEvent());

        Assert.Equal(2, callCount);
    }

    [Fact]
    public void Disposed_subscription_no_longer_receives_events()
    {
        var bus = new EventBus();
        int callCount = 0;
        var sub = bus.Subscribe<TestEvent>(_ => callCount++);

        sub.Dispose();
        bus.Publish(new TestEvent());

        Assert.Equal(0, callCount);
    }

    [Fact]
    public void Publishing_with_no_subscribers_does_not_throw()
    {
        var bus = new EventBus();

        var exception = Record.Exception(() => bus.Publish(new TestEvent()));

        Assert.Null(exception);
    }
}
