using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : UI
{
	public GameObject enemyCombatTextPrefab;

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;

	public GameObject barOne;
	public GameObject barTwo;
	public GameObject barThree;
	public GameObject barFour;
	public GameObject barFive;
	public GameObject barSix;
	public GameObject barSeven;
	public GameObject barGameObject;

	Button abilityOne;
	Button abilityTwo;
	Button abilityThree;
	Button abilityFour;
	Button abilityFive;
	Button abilitySix;
	Button abilitySeven;


	Text fps;
	float time;
	int counter = 0;

	void Start ()
	{
		Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);
		fps = GameObject.Find ("FPSCounter").GetComponent<Text> ();
		UpdateButtons ();
	}

	void UpdateButtons ()
	{
		abilityOne = barOne.transform.GetChild (0).GetComponent<Button> ();

	}

	void Update ()
	{
		Fps ();
		PressedButtons ();
	}

	void Fps ()
	{
		time += Time.deltaTime;


		if (time > 1f) {
			time = 0;
			fps.text = counter.ToString ();
			counter = 0;
		} else
			counter++;
	}

	void PressedButtons ()
	{

		if (Input.GetButton ("One")) {
			abilityOne.onClick.Invoke ();
			abilityOne.Select ();
		}

	}


	public void InitiateEnemyCombatText (float amount, bool ability, bool crit)
	{
		GameObject combatText = Instantiate (enemyCombatTextPrefab) as GameObject;
		RectTransform rectTransform = combatText.GetComponent<RectTransform> ();
	
		combatText.transform.SetParent (transform);
		rectTransform.transform.localPosition = enemyCombatTextPrefab.transform.localPosition;
		rectTransform.transform.localScale = enemyCombatTextPrefab.transform.localScale;
		rectTransform.transform.localRotation = combatText.transform.localRotation;
		Text text = combatText.GetComponent<Text> ();
		if (crit) {
			text.fontSize = (text.fontSize * 2);
			combatText.GetComponent<Animator> ().SetTrigger ("Crit");
			text.color = Color.yellow;
		} else {
			combatText.GetComponent<Animator> ().SetTrigger ("Damage");
		}
		if (ability)
			text.color = Color.yellow;
		text.text = ((int)(amount)).ToString ();
		Destroy (combatText, 1f);
	}

}
