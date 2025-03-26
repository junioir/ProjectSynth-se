
using UnityEngine;

public enum States
{
    Idle, Patrol, Pursuit
};

public class StateMachine : MonoBehaviour
{
    private BaseState _currentState;

    public void ChangeState(BaseState State) 
    {
        //Call the Exit function on the current state
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        // Change the current state to the new state
        _currentState = State;
        // Call the Enter function on the new state
        _currentState.Enter();
    }

    public BaseState GetCurrentState()
    {
        return _currentState;
    }

    private void Update()
    {
        if (_currentState != null)
        {
            // Call the Update function on the current state
            _currentState.Update();
        }
    }
}
