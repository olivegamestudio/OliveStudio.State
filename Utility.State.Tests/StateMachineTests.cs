using Musts;

namespace Utility.StateMachine.Tests;

public class StateMachineTests
{
    enum TestStates
    {
        Start,
        End
    }

    [Fact]
    public async Task Test1()
    {
        bool entered = false;
        bool exited = false;

        var stateMachine
            = new StateMachine<TestStates>(TestStates.Start)
                .State(TestStates.End)
                .OnEnter((state) =>
                {
                    if (state == TestStates.End)
                    {
                        entered = true;
                    }
                })
                .EndConfigure()
                .State(TestStates.Start)
                .OnExit((state) =>
                {
                    if (state == TestStates.Start)
                    {
                        exited = true;
                    }
                })
                .EndConfigure();

        // we are in the initial state and no action has been fired.
        stateMachine.IsInState(TestStates.Start).MustBeTrue();

        // change to the new state
        await stateMachine.ChangeState(TestStates.End);

        // we've changed to the new state and fired the OnEnter action.
        stateMachine.IsInState(TestStates.End).MustBeTrue();
        entered.MustBeTrue();
        exited.MustBeTrue();
    }
}
