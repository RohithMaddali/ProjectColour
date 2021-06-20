using System;

[Serializable]
public class DelegateState
{
	public Action Enter; //Start functions
	public Action Update; //Update functions
	public Action Exit; //Exit functions
	public bool   active; //Determines when the state ends
}