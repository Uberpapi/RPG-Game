using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
	[System.NonSerialized]
	public CharacterController player;
	[System.NonSerialized]
	public PlayerBehaviour myBehaviour;
	[System.NonSerialized]
	public Animator animator;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<CharacterController> ();
		myBehaviour = GetComponent<PlayerBehaviour> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
		
}
