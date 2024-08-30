namespace Utility;

/// <summary>
/// Represents a state in the state machine.
/// </summary>
/// <typeparam name="TEnum">The type of the state enumeration.</typeparam>
public class StateMachineState<TEnum> where TEnum : Enum
{
    readonly StateMachine<TEnum> _parentState;

    Action<TEnum>? _onEnter;

    Action<TEnum>? _onExit;

    /// <summary>
    /// Initializes a new instance of the <see cref="StateMachineState{TEnum}"/> class.
    /// </summary>
    /// <param name="parentState">The parent state machine.</param>
    public StateMachineState(StateMachine<TEnum> parentState)
    {
        _parentState = parentState;
    }

    /// <summary>
    /// Invokes the on-enter action for the state.
    /// </summary>
    /// <param name="value">The state value.</param>
    internal void FireOnEnter(TEnum value)
    {
        _onEnter?.Invoke(value);
    }

    /// <summary>
    /// Invokes the on-exit action for the state.
    /// </summary>
    /// <param name="value">The state value.</param>
    internal void FireOnExit(TEnum value)
    {
        _onExit?.Invoke(value);
    }

    /// <summary>
    /// Sets the action to be executed when entering the state.
    /// </summary>
    /// <param name="action">The action to execute on enter.</param>
    /// <returns>The current <see cref="StateMachineState{TEnum}"/> instance.</returns>
    public StateMachineState<TEnum> OnEnter(Action<TEnum> action)
    {
        _onEnter = action;
        return this;
    }

    /// <summary>
    /// Sets the action to be executed when exiting the state.
    /// </summary>
    /// <param name="action">The action to execute on exit.</param>
    /// <returns>The current <see cref="StateMachineState{TEnum}"/> instance.</returns>
    public StateMachineState<TEnum> OnExit(Action<TEnum> action)
    {
        _onExit = action;
        return this;
    }

    /// <summary>
    /// Ends the configuration of the state and returns the parent state machine.
    /// </summary>
    /// <returns>The parent <see cref="StateMachine{TEnum}"/> instance.</returns>
    public StateMachine<TEnum> EndConfigure()
    {
        return _parentState;
    }
}
