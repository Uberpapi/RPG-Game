using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targeting : UI
{
	public Camera playerCamera;
	public Slider targetEnemyHealthSlider;
	public Slider targetEnemyManaSlider;
	public Slider targetFriendlyHealthSlider;
	public Slider targetFriendlyManaSlider;

	BasicCombat playerCombat;
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
		
		targetPortrait = GameObject.Find ("EnemyTargetPortrait").GetComponent<Image> ().sprite;
		friendlyPortrait = GameObject.Find ("FriendlyTargetPortrait").GetComponent<Image> ().sprite;
		targetName = GameObject.Find ("EnemyName").GetComponent<Text> ();
		friendlyName = GameObject.Find ("FriendlyName").GetComponent<Text> ();
		MyCamera = playerCamera;
		Initiate ();
		StartCoroutine (UpdateRangeToTarget ());

		playerCombat = Player.GetComponent<BasicCombat> ();
		playerCollider = Player.GetComponent<Collider> ();
		StartCoroutine (LooseTarget ());
	}

	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			ray = myCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.transform.tag == "Enemy") {
					Target = hit.transform.gameObject;
					FriendlyTargetFrame.SetActive (false);
					EnemyTargetFrame.SetActive (true);
					EnemyBehaviour = hit.transform.GetComponent<EnemyBehaviour> ();
					// targetPortrait = hit.transform.GetComponent<Image> ().sprite;
					targetName.text = hit.transform.name;
					StartCoroutine (UpdateEnemyHealthBar ());
				} else if (hit.transform.tag == "Player" && hit.transform.gameObject != Player) {
					Target = hit.transform.gameObject;
					EnemyTargetFrame.SetActive (false);
					FriendlyTargetFrame.SetActive (true);
					PlayerBehaviour = hit.transform.GetComponent<PlayerBehaviour> ();
					//friendlyPortrait = hit.transform.GetComponent<Image> ().sprite;
					friendlyName.text = hit.transform.name;
					StartCoroutine (UpdateFriendlyHealthBar ());
				} else if (UIHitOrNot ()) {
					Target = null;
					EnemyTargetFrame.SetActive (false);
					FriendlyTargetFrame.SetActive (false);
					StopCoroutine (UpdateFriendlyHealthBar ());
					StopCoroutine (UpdateEnemyHealthBar ());
				}
			}
		}
	}

	bool UIHitOrNot ()
	{
		if (RectTransformUtility.RectangleContainsScreenPoint (EnemyTargetFrame.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (FriendlyTargetFrame.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (ActionBars.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			return false;
		} else if (RectTransformUtility.RectangleContainsScreenPoint (PlayerTargetFrame.GetComponent<RectTransform> (), Input.mousePosition, null)) {
			Target = Player;
			friendlyName.text = Player.name;
			EnemyTargetFrame.SetActive (false);
			FriendlyTargetFrame.SetActive (true);
			return false;
		} else {
			return true;
		}
	}

	float GetRange (Transform target)
	{

		if (target != PlayerTargetFrame.transform) {
			Vector3 closestPoint = target.GetComponent<Collider> ().ClosestPointOnBounds (Player.transform.position);

			float distance = Vector3.Distance (closestPoint, Player.transform.position);

			return distance;
		} else
			return 0f;
	}

	IEnumerator UpdateRangeToTarget ()
	{

		while (true) {

			if (Target != null) {
				Range = GetRange (Target.transform);
			} else
				Range = Mathf.Infinity;

			yield return new WaitForSeconds (0.2f);
		}
	}

	IEnumerator UpdateEnemyHealthBar ()
	{

		while (true && EnemyBehaviour != null) {

			targetEnemyHealthSlider.maxValue = EnemyBehaviour.MaxHitpoint;
			targetEnemyHealthSlider.value = EnemyBehaviour.Hitpoint;

			yield return new WaitForSeconds (0.2f);
		}

	}

	IEnumerator UpdateFriendlyHealthBar ()
	{

		while (true && PlayerBehaviour != null) {

			targetFriendlyHealthSlider.maxValue = PlayerBehaviour.MaxHitpoint;
			targetFriendlyHealthSlider.value = PlayerBehaviour.Hitpoint;

			yield return new WaitForSeconds (0.2f);

		}

	}

	IEnumerator LooseTarget ()
	{
		while (true) {
			if (Target == null) {
				FriendlyTargetFrame.SetActive (false);
				EnemyTargetFrame.SetActive (false);
				StopCoroutine (UpdateFriendlyHealthBar ());
				StopCoroutine (UpdateEnemyHealthBar ());
			}

			yield return new WaitForSeconds (0.4f);
		}
	}
}
