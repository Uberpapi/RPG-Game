using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targeting : MonoBehaviour
{
	public Camera myCamera;

	GameObject enemyTarget;
	GameObject friendlyTarget;
	GameObject playerTarget;
	Sprite targetPortrait;
	Sprite friendlyPortrait;
	Text targetName;
	Text friendlyName;

	RaycastHit hit;
	Ray ray;

	// Use this for initialization
	void Start ()
	{
		enemyTarget = GameObject.Find ("EnemyFrame");
		friendlyTarget = GameObject.Find ("FriendlyFrame");
		playerTarget = GameObject.Find ("PlayerFrame");
		targetPortrait = GameObject.Find ("TargetPortrait").GetComponent<Image> ().sprite;
		friendlyPortrait = GameObject.Find ("FriendlyPortraitPicture").GetComponent<Image> ().sprite;
		targetName = GameObject.Find ("TargetName").GetComponent<Text> ();
		friendlyName = GameObject.Find ("FriendlyName").GetComponent<Text> ();
		enemyTarget.SetActive (false);
		friendlyTarget.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			ray = myCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.transform.tag == "Enemy") {
					friendlyTarget.SetActive (false);
					enemyTarget.SetActive (true);
					// targetPortrait = hit.transform.GetComponent<Image> ().sprite;
					targetName.text = hit.transform.name;
				} else if (hit.transform.tag == "Player") {
					enemyTarget.SetActive (false);
					friendlyTarget.SetActive (true);
					//friendlyPortrait = hit.transform.GetComponent<Image> ().sprite;
					friendlyName.text = hit.transform.name;
				} else if (!UIHitOrNot ()) {
					enemyTarget.SetActive (false);
					friendlyTarget.SetActive (false);
				}
			}
		}
	}

	bool UIHitOrNot ()
	{

		if (!RectTransformUtility.RectangleContainsScreenPoint (enemyTarget.GetComponent<RectTransform> (), Input.mousePosition, null))
			return false;
		else if (!RectTransformUtility.RectangleContainsScreenPoint (friendlyTarget.GetComponent<RectTransform> (), Input.mousePosition, null))
			return false;
		//else if (!RectTransformUtility.RectangleContainsScreenPoint (playerTarget.GetComponent<RectTransform> (), Input.mousePosition, null))
		//	return false;
		else
			return true;
	}
}
