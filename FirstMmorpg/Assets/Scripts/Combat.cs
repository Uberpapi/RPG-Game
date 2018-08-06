using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{

	BaseBehaviour myBehaviour;
	Animator animator;
	CharacterController player;


	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		myBehaviour = GetComponent <BaseBehaviour> ();
		player = GetComponent<CharacterController> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Strike ();
	}

	void Strike ()
	{
		if (Input.GetButtonDown ("Fire2") && myBehaviour.Energy > 50) {
			myBehaviour.Energy -= 50;
			int number = Random.Range (1, 3);
			animator.SetTrigger ("strike" + number);
		}
	}
}
