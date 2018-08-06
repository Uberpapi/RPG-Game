using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{

	const float maxEnergy = 100f;
	float energy = 100f;
	PlayerBehaviour myBehaviour;
	Animator animator;
	CharacterController player;


	float flashspeed = 10000f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.1f);
	public Slider energySlider;
	public Slider healthSlider;
	public Image damageImage;
	bool damaged = false;

	Text fps;
	float time;
	int counter = 0;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		myBehaviour = GetComponent < PlayerBehaviour> ();
		player = GetComponent<CharacterController> ();
		StartCoroutine (UpdateEnergyBar ());
		StartCoroutine (UpdateHealthBar ());
		fps = GameObject.Find ("FPSCounter").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Strike ();
		Fps ();

		/*if (damaged)
			damageImage.color = flashColour;
		else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashspeed * Time.deltaTime);
		}
			damaged = false;
			}*/
	}

	void Strike ()
	{
		if (Input.GetButtonDown ("Fire2") && energy > 50f) {
			energy -= 50f;
			int number = Random.Range (1, 3);
			animator.SetTrigger ("strike" + number);
			damaged = true;
		}
	}

	IEnumerator UpdateHealthBar ()
	{
		healthSlider.maxValue = myBehaviour.MaxHitpoint;

		while (true) {
			healthSlider.value = myBehaviour.Hitpoint;
			yield return new WaitForSecondsRealtime (0.5f);
		}
	}

	IEnumerator UpdateEnergyBar ()
	{
		
		while (true) {
			energy += 6;
			energySlider.value = energy;
			yield return new WaitForSecondsRealtime (0.3f);
		}
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
}
