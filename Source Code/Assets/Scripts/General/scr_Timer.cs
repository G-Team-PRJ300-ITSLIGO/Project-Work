using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Timer : MonoBehaviour {
	public Timer[] timers;


	// Use this for initialization
	void Start () 
	{
		foreach (Timer t in timers)
		{
			t.time = t.inactiveAt;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		foreach(Timer t in timers)
		{
			if(t.time == t.inactiveAt)
			{
				t.active = false;
			}
			else
			{
				t.active = true;
			}

			if(t.active)
			{
				t.time -= Time.deltaTime;
			}

			if(t.time <= 0.0f & t.active)
			{
				t.hasElapsed = true;
				t.active = false;
				t.time = t.inactiveAt;
			}
		}
			
	}


}
