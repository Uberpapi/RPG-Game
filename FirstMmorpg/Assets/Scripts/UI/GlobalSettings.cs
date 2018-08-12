using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : UI
{
	public GameObject incommingDamageTextPrefab;
	public GameObject outgoingDamageTextPrefab;
	public GameObject messagePrefab;

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
	GameObject textPanel;

	Button abilityOne;
	Button abilityTwo;
	Button abilityThree;
	Button abilityFour;
	Button abilityFive;
	Button abilitySix;
	Button abilitySeven;

	Targeting targeting;

	bool messageActive = false;
	Text fps;
	float time;
	int counter = 0;

	void Start ()
	{
		Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);
		fps = GameObject.Find ("FPSCounter").GetComponent<Text> ();
		textPanel = GameObject.Find ("TextPanel");
		targeting = GetComponent<Targeting> ();
		//messageText = message.GetComponent<Text> ();
		//messageAnim = message.GetComponent<Animator> ();
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
			if (targeting.Target == null) {
				StartCoroutine (MessageText ("You need a target", 1f));
			}
		}

	}


	public void InitiateCombatText (float amount, bool ability, bool crit, bool outgoing)
	{
		if (outgoing) {
			GameObject combatText = Instantiate (outgoingDamageTextPrefab) as GameObject;
			//RectTransform rectTransform = combatText.GetComponent<RectTransform> ();
			combatText.transform.SetParent (textPanel.transform);
			combatText.transform.localPosition = outgoingDamageTextPrefab.transform.localPosition;
			combatText.transform.localScale = outgoingDamageTextPrefab.transform.localScale;
			combatText.transform.localRotation = outgoingDamageTextPrefab.transform.localRotation;	
			Text text = combatText.GetComponent<Text> ();
			if (crit) {
				text.fontSize = (text.fontSize * 2);
				combatText.GetComponent<Animator> ().SetTrigger ("Crit");
			} else {
				combatText.GetComponent<Animator> ().SetTrigger ("Damage");
			}
			if (ability)
				text.color = Color.yellow;
			text.text = ((int)(amount)).ToString ();
			Destroy (combatText, 1.2f);
		} else {
			
			GameObject combatText = Instantiate (incommingDamageTextPrefab) as GameObject;
			//RectTransform rectTransform = combatText.GetComponent<RectTransform> ();
			combatText.transform.SetParent (textPanel.transform);
			combatText.transform.localPosition = incommingDamageTextPrefab.transform.localPosition;
			combatText.transform.localScale = incommingDamageTextPrefab.transform.localScale;
			combatText.transform.localRotation = incommingDamageTextPrefab.transform.localRotation;	
			Text text = combatText.GetComponent<Text> ();
			if (crit) {
				text.fontSize = (text.fontSize * 2);
				combatText.GetComponent<Animator> ().SetTrigger ("Crit");
			} else {
				combatText.GetComponent<Animator> ().SetTrigger ("Damage");
			}
			if (ability)
				text.color = Color.yellow;
			text.text = ("-" + ((int)(amount)).ToString ());
			Destroy (combatText, 1.2f);

		}
	}

	public IEnumerator MessageText (string text, float duration)
	{
		if (!messageActive) {
			messageActive = true;
			GameObject message = Instantiate (messagePrefab) as GameObject;
			message.transform.SetParent (textPanel.transform);
			RectTransform rectTransform = message.GetComponent<RectTransform> ();

			message.transform.localPosition = messagePrefab.transform.localPosition;
			message.transform.localScale = messagePrefab.transform.localScale;
			message.transform.localRotation = messagePrefab.transform.localRotation;

			Text messageText = message.GetComponent<Text> ();
			messageText.text = text;
			//message.GetComponent<Animator> ().SetTrigger ("Message");
			yield return new WaitForSeconds (duration);
			Destroy (message);
			messageActive = false;
		}


	}

}
