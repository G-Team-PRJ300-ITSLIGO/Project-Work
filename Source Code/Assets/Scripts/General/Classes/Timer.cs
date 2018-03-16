using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Timer
{
	public float time;
	public float inactiveAt;
	public bool hasElapsed;
	public bool active;

	public Timer()
	{
		
	}

	public void CheckIfActive()
	{
		if(time == inactiveAt)
		{
			active = false;
		}	
		else
		{
			active = true;
		}
	}

	public void SetTime (float amount)
	{
		time = amount;
	}

	public void SetRandomTime(float minVal, float maxVal)
	{
		time =  Random.Range (minVal, maxVal);
	}

}
