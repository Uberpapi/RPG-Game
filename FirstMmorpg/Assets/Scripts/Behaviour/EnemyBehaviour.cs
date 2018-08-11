﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BaseBehaviour
{

	Animator anim;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		Movement = GetComponent<UnitMovement> ();
		StartEnemyRoutines ();
		StartCoroutine (autoAttack ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	[SerializeField]
	protected int experience;

	public int Experience {
		get { return experience; }
		set { experience = value; }
	}

	protected UnitMovement movement;

	public UnitMovement Movement {
		get { return movement; }
		set { movement = value; }
	}


	public IEnumerator autoAttack ()
	{

		while (true) {
			if (Movement.Target != null) {
				anim.SetTrigger ("strike1");
				yield return new WaitForSeconds (0.2f);
				Movement.Target.GetComponent<PlayerBehaviour> ().ApplyDamage (BaseDamageMin, BaseDamageMax, CritChance, gameObject, false);
				yield return new WaitForSeconds (AttackSpeed);
			} else
				yield return new WaitForSeconds (0.2f);
		}
	}
}
