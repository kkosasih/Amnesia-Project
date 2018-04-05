using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour {
    #region Attributes
    public int startHour = 6;
	public int startMinute = 0;
	public List<byte> earlyMorning;
	public List<byte> noon;
	public List<byte> sunSet;
	public List<byte> night;
	public List<byte> lateNight;
	private int hour = 0;
	private int minute = 0;
	public float howLongTillIncrement = 10f; //seconds, to finish a full day = 24 minutes.
	private float previousCounter = 0f;
	private float counter = 0f;
	private Text clock;
	private Image DayNight;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start () 
	{
		hour = startHour;
		minute = startMinute;
		clock = GameObject.Find ("Clock").GetComponent<Text>();	
		DayNight = GameObject.Find ("DayNightPanel").GetComponent<Image> ();

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

		if (hour >= 6)
		{
			DayNight.color = new Color32 (earlyMorning [0], earlyMorning [1], earlyMorning [2], earlyMorning [3]);
		}
		else
		if (hour >= 12)
		{
			DayNight.color = new Color32 (noon [0], noon [1], noon [2], noon [3]);
		}
		else
		if (hour >= 17)
		{
			DayNight.color = new Color32 (sunSet [0], sunSet [1], sunSet [2], sunSet [3]);
		}
		else
		if (hour >= 21)
		{
			DayNight.color = new Color32 (night [0], night [1], night [2], night [3]);
		}
		else
		if (hour >= 0)
		{
			DayNight.color = new Color32 (lateNight [0], lateNight [1], lateNight [2], lateNight [3]);
		}
	}
    #endregion

    #region Methods
    public int getHour()
	{
		return hour;
	}

	public int getMinute()
	{
		return minute;
	}
    #endregion
}
