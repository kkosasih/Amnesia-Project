using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour {

	public int startHour = 6;
	public int startMinute = 0;
	private int hour = 0;
	private int minute = 0;
	public float howLongTillIncrement = 10f; //seconds, to finish a full day = 24 minutes.
	private float previousCounter = 0f;
	private float counter = 0f;
	private Text clock;

	// Use this for initialization
	void Start () 
	{
		hour = startHour;
		minute = startMinute;
		clock = GameObject.Find ("Clock").GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {

		if (counter - previousCounter >= howLongTillIncrement)
		{
			previousCounter = Time.time;
			if (minute < 5)
			{
				minute++;
			}
			else
			if (hour < 23)
			{
				minute = 0;
				hour++;
			}
			else
			{
				hour = 0;
				minute = 0;
			}
		}

		counter = Time.time;

		if (hour < 10)
		{
			clock.text = "0" + hour + ":" + minute + "0";
		}
		else
		{
			clock.text = hour + ":" + minute + "0";
		}
	}

	public int getHour()
	{
		return hour;
	}

	public int getMinute()
	{
		return minute;
	}
}
