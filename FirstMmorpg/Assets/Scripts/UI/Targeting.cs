using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targeting : UI
{
	public Camera playerCamera;

	Sprite targetPortrait;
	Sprite friendlyPortrait;
	Text targetName;
	Text friendlyName;
	Collider playerCollider;

	RaycastHit hit;
	Ray ray;

	protected float range = 100f;

	public float Range {
		get { return range; }
		set { range = value; }
	}

	// Use this for initialization
	void Start ()
	{
		targetPortrait = GameObject.Find ("TargetPortrait").GetComponent<Image> ().sprite;
		friendlyPortrait = GameObject.Find ("FriendlyPortraitPicture").GetComponent<Image> ().sprite;
		targetName = GameObject.Find ("TargetName").GetComponent<Text> ();
		friendlyName = GameObject.Find ("FriendlyName").GetComponent<Text> ();
		MyCamera = playerCamera;

		StartCoroutine (UpdateRangeToTarget ());
		Initiate ();
		playerCollider = Player.GetComponent<Collider> ();
	}

	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			ray = myCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.transform.tag == "Enemy") {
					Target = hit.transform.gameObject;
					FriendlyTarget.SetActive (false);
					EnemyTarget.SetActive (true);
					// targetPortrait = hit.transform.GetComponent<Image> ().sprite;
					targetName.text = hit.transform.name;
				} else if (hit.transform.tag == "Player") {
					Target = hit.transform.gameObject;
					EnemyTarget.SetActive (false);
					FriendlyTarget.SetActive (true);
					//friendlyPortrait = hit.transform.GetComponent<Image> ().sprite;
					friendlyName.text = hit.transform.name;
				} else if (UIHitOrNot ()) {
					Target = null;
					EnemyTarget.SetActive (false);
					FriendlyTarget.SetActive (false);
				}
			}
		}
	}

	bool UIHitOrNot ()
	{
		if (RectTransformUtility.RectangleContainsScreenPoint (EnemyTarget.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (FriendlyTarget.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (ActionBars.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (PlayerTarget.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else {
			return true;
		}
	}

	float GetRange (Transform target)
	{
		
		Vector3 closestPoint = target.GetComponent<Collider> ().ClosestPointOnBounds (Player.transform.position);

		float distance = Vector3.Distance (closestPoint, Player.transform.position);

		return distance;
	}

	IEnumerator UpdateRangeToTarget ()
	{

		while (true) {

			if (Target != null) {
				Range = GetRange (Target.transform);
				//print (Range);
			} else
				Range = Mathf.Infinity;


			yield return new WaitForSeconds (0.1f);
			print (BarOne.name);
		}
	}
}
