using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : BaseController
{

	// Use this for initialization
	void Start ()
	{
		//hitpoint = MaxHitpoint;
		StartCoroutine (HitpointReg ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	[SerializeField]
	protected float hitpoint = 10f;

	public float Hitpoint {
		set { hitpoint = value; }
		get { return hitpoint; }
	}

	//Max Health
	[SerializeField]
	protected float maxHitPoint = 100f;

	public float MaxHitpoint {
		set { maxHitPoint = value; }
		get { return maxHitPoint; }
	}

	protected float bonusDamage;

	public float BonusDamage {
		get { return bonusDamage; }
		set { bonusDamage = value; }
	}

	//The damage of a unit per hit
	[SerializeField]
	[Tooltip ("Damage per attack")]
	protected float baseDamage = 1f;

	public float BaseDamage {
		set { baseDamage = value; }
		get { return baseDamage; }
	}

	//The range a unit can attack from
	[SerializeField]
	[Tooltip ("Attack range in coordinate units")]
	protected int attackRange = 1;

	public int AttackRange {
		set { attackRange = value; }
		get { return attackRange; }
	}

	//Time between attacks
	[SerializeField]
	[Tooltip ("Time between attacks in seconds")]
	protected float attackSpeed = 1;

	public float AttackSpeed {
		set { attackSpeed = value; }
		get { return attackSpeed; }
	}

	[SerializeField]
	protected float hpReg = 0f;
	//Healt Regen per second
	public float HpReg {
		set { hpReg = value; }
		get { return hpReg; }
	}

	IEnumerator HitpointReg ()
	{
		while (true) {
			if (Hitpoint < MaxHitpoint)
				Hitpoint += HpReg;
			else
				hitpoint = maxHitPoint;

			yield return new WaitForSecondsRealtime (1f);
		}
	}
}
