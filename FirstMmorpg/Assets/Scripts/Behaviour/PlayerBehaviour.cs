using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : BaseBehaviour
{
	public Slider expSlider;

	// Use this for initialization
	void Start ()
	{
		StartRoutines ();
		UpdateLevel ();
		expSlider.maxValue = ExpToNextLevel;
		expSlider.value = Experience;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	protected int expToNextLevel = 400;

	public int ExpToNextLevel {
		
		get { return expToNextLevel; }
		set { expToNextLevel = value; }

	}

	protected int experience = 42;

	public int Experience {

		get { return experience; }
		set { experience = value; }
	}

	public void UpdateExperince (int expGain)
	{
		Experience += expGain;

		if (Experience > ExpToNextLevel) {

			//animator.play(DING);
			int overflow = Experience - ExpToNextLevel;
			Level += 1;
			//ExpToNextLevel = GetNextLevelExp ();
			Experience = overflow;
			expSlider.maxValue = ExpToNextLevel;
			expSlider.value = Experience;
		} else {
			expSlider.value = Experience;
		}
		UpdateLevel ();
	}

	public void UpdateLevel ()
	{
		Text playerLevel = GameObject.Find ("PlayerLevel").GetComponent<Text> ();
		playerLevel.text = Level.ToString ();
	}
}
