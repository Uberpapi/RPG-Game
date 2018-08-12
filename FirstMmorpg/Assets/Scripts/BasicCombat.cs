using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicCombat : MonoBehaviour
{
	
	Animator animator;
	CharacterController controller;
	UI userInterface;
	float autoAttackTimer;
	GlobalSettings globalSettings;

	//angle decides the minimum angle a target is considered to be infront of player/npc
	int angle = 50;

	// Use this for initialization
	void Start ()
	{
		userInterface = GameObject.Find ("UserInterface").GetComponent<UI> ();
		Targets = userInterface.GetComponent<Targeting> ();
		globalSettings = userInterface.GetComponent<GlobalSettings> ();
		animator = GetComponent<Animator> ();
		PlayerBehaviour = GetComponent <PlayerBehaviour> ();
		controller = GetComponent<CharacterController> ();
		StartCoroutine (UpdateRange ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		autoAttackTimer += Time.deltaTime;

		if (targets.Target != null && targets.Target.tag == "Enemy") {
			if (Input.GetButtonDown ("Fire2")) {
				Auto = true;
			}

			if (Auto && Targets.EnemyBehaviour != null) {
				AutoAttack ();
			}
		} else {
			Auto = false;
		}
	}

	protected float range = 100f;

	public float Range {
		get { return range; }
		set { range = value; }
	}

	protected Targeting targets;

	public Targeting Targets {
		get { return targets; }
		set { targets = value; }
	}

	//If autocombat needs to be shutoff just set this to false
	public bool auto = false;

	public bool Auto {
		get { return auto; }
		set { auto = value; }
	}

	protected PlayerBehaviour playerBehaviour;

	public PlayerBehaviour PlayerBehaviour {
		get { return playerBehaviour; }
		set { playerBehaviour = value; }
	}


	public void AutoAttack ()
	{
		Auto = true;
		if (tag == "Player") {
			if (PlayerBehaviour.AttackRange > Range) {
				if (PlayerBehaviour.AttackSpeed < autoAttackTimer && Range < PlayerBehaviour.AttackRange) {
					if (FacingTarget ()) {
						//int number = Random.Range (1, 3);
						animator.SetTrigger ("strike1");
						if (Targets.EnemyBehaviour != null) {
							Targets.EnemyBehaviour.ApplyDamage (PlayerBehaviour.BaseDamageMin, PlayerBehaviour.BaseDamageMax, PlayerBehaviour.CritChance, gameObject, false);
						}
						autoAttackTimer = 0f;
					} else {
						StartCoroutine (globalSettings.MessageText ("Not facing target", 1f));
						print ("Not facing Target");
						// Sätt in print i Text animation i skärmen
					}
				}
			} else {
				StartCoroutine (globalSettings.MessageText ("Target is out of range", 1.5f));
			}
		}
	}

	public bool FacingTarget ()
	{
		Vector3 direction = Targets.Target.transform.position - transform.position;
		float angleToTarget = Vector3.Angle (direction, transform.forward);

		if (angleToTarget < angle) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator UpdateRange ()
	{
		while (true) {
			
			Range = targets.Range;
			yield return new WaitForSeconds (0.2f);
		}
	}
}
