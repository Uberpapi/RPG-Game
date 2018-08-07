using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BaseBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartEnemyRoutines ();
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
}
