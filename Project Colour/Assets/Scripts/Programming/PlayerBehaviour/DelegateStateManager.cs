public class DelegateStateManager
{
	//Settable state so we know what is currently the state
	public DelegateState currentState;

	//Function to change states
	public void ChangeState(DelegateState newState)
	{
		//If the current state exists
		if (currentState != null)
		{
			//Set it to inactive then exit, this breaks the update loop
			currentState.active = false;
			currentState.Exit?.Invoke();
		}
		
		//Then if the new state exists (POTENTIAL CRASH)
		if (newState != null)
		{
			//Set it to active and run the Enter as a Start functions, we then overlap the current state with it
			newState.active = true;
			newState.Enter?.Invoke();
			currentState = newState;
		}
	}

	//Update loop, call this to make update... Update.
	public void UpdateCurrentState()
	{
		currentState?.Update?.Invoke();
	}
}