using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

	public void Initiate ()
	{
		EnemyTargetFrame = GameObject.Find ("EnemyFrame");
		FriendlyTargetFrame = GameObject.Find ("FriendlyFrame");
		PlayerTargetFrame = GameObject.Find ("PlayerFrame");
		Player = GameObject.Find ("Player");
		ActionBars = GameObject.Find ("ActionBars");

		//FindBars ();

		EnemyTargetFrame.SetActive (false);
		FriendlyTargetFrame.SetActive (false);
	}


	protected GameObject actionBars;

	public GameObject ActionBars {
		get { return actionBars; }
		set { actionBars = value; }
	}

	protected GameObject enemyTargetFrame;

	public GameObject EnemyTargetFrame {
		get { return enemyTargetFrame; }
		set { enemyTargetFrame = value; }
	}

	protected GameObject friendlyTargetFrame;

	public GameObject FriendlyTargetFrame {
		get { return friendlyTargetFrame; }
		set { friendlyTargetFrame = value; }
	}

	protected GameObject playerTargetFrame;

	public GameObject PlayerTargetFrame {
		get { return playerTargetFrame; }
		set { playerTargetFrame = value; }
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

	protected PlayerBehaviour playerBehaviour;

	public PlayerBehaviour PlayerBehaviour {
		get { return playerBehaviour; }
		set { playerBehaviour = value; }
	}

	protected EnemyBehaviour enemyBehaviour;

	public EnemyBehaviour EnemyBehaviour {
		get { return enemyBehaviour; }
		set { enemyBehaviour = value; }
	}
}


/*
	public void FindBars ()
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
	*/


/*
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
	*/



