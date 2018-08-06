using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
	[System.NonSerialized]
	public CharacterController player;
	[System.NonSerialized]
	public BaseBehaviour myBehaviour;
	[System.NonSerialized]
	public Animator animator;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<CharacterController> ();
		myBehaviour = GetComponent<BaseBehaviour> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
		
}
