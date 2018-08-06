using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

	public void Initiate ()
	{
		EnemyTarget = GameObject.Find ("EnemyFrame");
		FriendlyTarget = GameObject.Find ("FriendlyFrame");
		PlayerTarget = GameObject.Find ("PlayerFrame");
		Player = GameObject.Find ("Player");

		FindBars ();

		EnemyTarget.SetActive (false);
		FriendlyTarget.SetActive (false);
	}


	protected GameObject actionBars;

	public GameObject ActionBars {
		get { return actionBars; }
		set { actionBars = value; }
	}

	protected GameObject enemyTarget;

	public GameObject EnemyTarget {
		get { return enemyTarget; }
		set { enemyTarget = value; }
	}

	protected GameObject friendlyTarget;

	public GameObject FriendlyTarget {
		get { return friendlyTarget; }
		set { friendlyTarget = value; }
	}

	protected GameObject playerTarget;

	public GameObject PlayerTarget {
		get { return playerTarget; }
		set { playerTarget = value; }
	}

	protected GameObject player;

	public GameObject Player {
		get { return player; }
		set { player = value; }
	}

	protected Camera myCamera;

	public Camera MyCamera {
		get { return myCamera; }
		set { myCamera = value; }
	}

	protected GameObject target;

	public GameObject Target {
		get { return target; }
		set { target = value; }
	}

	protected GameObject barOne;

	public GameObject BarOne {
		get { return barOne; }
		set { barOne = value; }
	}

	protected GameObject barTwo;

	public GameObject BarTwo {
		get { return barTwo; }
		set { barTwo = value; }
	}

	protected GameObject barThree;

	public GameObject BarThree {
		get { return barThree; }
		set { barThree = value; }
	}

	protected GameObject barFour;

	public GameObject BarFour {
		get { return barFour; }
		set { barFour = value; }
	}

	protected GameObject barFive;

	public GameObject BarFive {
		get { return barFive; }
		set { barFive = value; }
	}

	protected GameObject barSix;

	public GameObject BarSix {
		get { return barSix; }
		set { barSix = value; }
	}

	protected GameObject barSeven;

	public GameObject BarSeven {
		get { return barSeven; }
		set { barSeven = value; }
	}

	protected GameObject settingsBar;

	public GameObject SettingsBar {
		get { return settingsBar; }
		set { settingsBar = value; }
	}

	void FindBars ()
	{
		ActionBars = GameObject.Find ("ActionBars");
		BarOne = GameObject.Find ("Bar1");
		BarTwo = GameObject.Find ("Bar2");
		BarThree = GameObject.Find ("Bar3");
		BarFour = GameObject.Find ("Bar4");
		BarFive = GameObject.Find ("Bar5");
		BarSix = GameObject.Find ("Bar6");
		BarSeven = GameObject.Find ("Bar7");
		SettingsBar = GameObject.Find ("SettingsBar");
	}
}




