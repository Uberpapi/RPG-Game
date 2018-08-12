using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	Text enemyLevel;
	Text friendlyLevel;
	Text enemyName;
	Text friendlyName;

	Text enemyHealthPercentage;
	Text playerHealthPercentage;
	Text friendlyHealthPercentage;

	Sprite targetPortrait;
	Sprite friendlyPortrait;


	public void Initiate ()
	{
		EnemyTargetFrame = GameObject.Find ("EnemyFrame");
		FriendlyTargetFrame = GameObject.Find ("FriendlyFrame");
		PlayerTargetFrame = GameObject.Find ("PlayerFrame");
		Player = GameObject.Find ("Player");
		PlayerBehaviour = Player.GetComponent<PlayerBehaviour> ();
		ActionBars = GameObject.Find ("ActionBars");
		enemyLevel = GameObject.Find ("EnemyLevel").GetComponent<Text> ();
		friendlyLevel = GameObject.Find ("FriendlyLevel").GetComponent<Text> ();
		targetPortrait = GameObject.Find ("EnemyTargetPortrait").GetComponent<Image> ().sprite;
		friendlyPortrait = GameObject.Find ("FriendlyTargetPortrait").GetComponent<Image> ().sprite;
		enemyName = GameObject.Find ("EnemyName").GetComponent<Text> ();
		friendlyName = GameObject.Find ("FriendlyName").GetComponent<Text> ();
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

	protected NpcBehaviour friendlyBehaviour;

	public NpcBehaviour FriendlyBehaviour {
		get { return friendlyBehaviour; }
		set { friendlyBehaviour = value; }
	}

	protected NpcBehaviour enemyBehaviour;

	public NpcBehaviour EnemyBehaviour {
		get { return enemyBehaviour; }
		set { enemyBehaviour = value; }
	}



	public void UpdateTargetInfo (string targetType)
	{
		if (Target != null) {
			if (targetType == "Enemy") {
				EnemyBehaviour = Target.GetComponent<NpcBehaviour> ();
				enemyLevel.text = EnemyBehaviour.Level.ToString ();
				enemyName.text = Target.name;
				// targetPortrait = hit.transform.GetComponent<Image> ().sprite;
			} else if (targetType == "Friendly") {
				FriendlyBehaviour = Target.GetComponent<NpcBehaviour> ();
				friendlyLevel.text = FriendlyBehaviour.Level.ToString ();
				friendlyName.text = Target.name;
				//friendlyPortrait = hit.transform.GetComponent<Image> ().sprite;
			} else {

				friendlyLevel.text = PlayerBehaviour.Level.ToString ();
				friendlyName.text = PlayerBehaviour.name;

			}
		}
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



