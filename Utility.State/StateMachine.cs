namespace Utility;

/// <summary>
/// Represents a state machine that manages states of type <typeparamref name="TEnum"/>.
/// </summary>
/// <typeparam name="TEnum">The type of the state enumeration.</typeparam>
public class StateMachine<TEnum> where TEnum : Enum
{
    readonly Dictionary<TEnum, StateMachineState<TEnum>> _states = new();

    /// <summary>
    /// Gets the current state of the state machine.
    /// </summary>
    public TEnum CurrentState { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateMachine{TEnum}"/> class with the specified initial state.
    /// </summary>
    /// <param name="initialState">The initial state of the state machine.</param>
    public StateMachine(TEnum initialState)
    {
        CurrentState = initialState;
    }

    /// <summary>
    /// Adds a new state to the state machine.
    /// </summary>
    /// <param name="value">The state to add.</param>
    /// <returns>The <see cref="StateMachineState{TEnum}"/> associated with the added state.</returns>
    public StateMachineState<TEnum> State(TEnum value)
    {
        StateMachineState<TEnum> state = new(this);
        _states.Add(value, state);
        return state;
    }

    /// <summary>
    /// Changes the current state of the state machine to the specified new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(TEnum newState)
    {
        if (_states.TryGetValue(CurrentState, out StateMachineState<TEnum> exitState))
        {
            exitState.FireOnExit(CurrentState);
        }

        // transition to new state
        if (_states.TryGetValue(newState, out StateMachineState<TEnum> state))
        {
            CurrentState = newState;
            state.FireOnEnter(newState);
        }
    }
}
