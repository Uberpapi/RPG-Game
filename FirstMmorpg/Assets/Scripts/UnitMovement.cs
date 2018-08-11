using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{

	NavMeshAgent myAgent;
	Animator myAnimator;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 patrolDestination1;
	private Vector3 patrolDestination2;
	private Vector3 aggroPosition = Vector3.zero;
	private Collider[] playersWithinRange;
	[SerializeField]
	private int aggroRange = 20;
	[SerializeField]
	private float agentSpeed = 3.5f;
	[SerializeField]
	private float agentAcceleration = 100;
	[SerializeField]
	private float agentAngularSpeed = 360;
	[SerializeField]
	private float agentStoppingDistance = 5;
	int layerMask = 1 << 9;
	UnitSpawner unitSpawner;

	void Start ()
	{
		unitSpawner = transform.parent.GetComponent<UnitSpawner> ();
		myAgent = GetComponent<NavMeshAgent> ();
		if (myAgent == null) {
			gameObject.AddComponent<NavMeshAgent> ();
			myAgent = GetComponent<NavMeshAgent> ();
		}
		myAgent.speed = agentSpeed;
		myAgent.radius = 0.01f;
		myAgent.acceleration = agentAcceleration;
		myAgent.angularSpeed = agentAngularSpeed;
		myAgent.stoppingDistance = agentStoppingDistance;
		myAnimator = GetComponent<Animator> ();
		if (!myAgent.isOnNavMesh) {
			print ("Unit did not spawn on navmesh, destroying it");
			Destroy (gameObject);
		} else {
			StartCoroutine (UpdateMovement ());
		}

	}


	IEnumerator UpdateMovement ()
	{
		while (true) {
			playersWithinRange = Physics.OverlapSphere (transform.position, aggroRange, layerMask);
			if (playersWithinRange.Length != 0) {
				if (aggroPosition == Vector3.zero) {
					aggroPosition = transform.position;
					myAgent.speed = 10;
					myAnimator.SetBool ("walk", false);
					myAnimator.SetBool ("run", true);
				} else if (myAgent.remainingDistance < 5f) {
					myAnimator.SetBool ("walk", false);
					myAnimator.SetBool ("run", false);
				} else {
					myAnimator.SetBool ("walk", false);
					myAnimator.SetBool ("run", true);
				}
				myAgent.destination = playersWithinRange [0].transform.position;
			} else if (myAgent.remainingDistance < 5f && aggroPosition == Vector3.zero) {
				myAgent.speed = 3.5f;
				myAnimator.SetBool ("walk", true);
				myAnimator.SetBool ("run", false);
				myAgent.destination = unitSpawner.RandomPositionOnMesh ();
			} else if (aggroPosition != Vector3.zero) {
				playersWithinRange = null;
				myAgent.speed = 20;
				myAnimator.SetBool ("walk", false);
				myAnimator.SetBool ("run", true);
				myAgent.destination = aggroPosition;
				aggroPosition = Vector3.zero;
				while (true) {
					yield return new WaitForSeconds (0.1f);
					if (myAgent.remainingDistance < 5f) {
						myAgent.speed = 3.5f;
						myAnimator.SetBool ("walk", true);
						myAnimator.SetBool ("run", false);
						myAgent.destination = unitSpawner.RandomPositionOnMesh ();
						break;
					}
				}
			}

			yield return new WaitForSeconds (0.1f);
		}
	}
}
