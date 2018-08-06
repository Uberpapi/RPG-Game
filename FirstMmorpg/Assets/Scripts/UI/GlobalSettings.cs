using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : UI
{

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;

	Text fps;
	float time;
	int counter = 0;

	void Start ()
	{
		Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);
		fps = GameObject.Find ("FPSCounter").GetComponent<Text> ();
	}

	void Update ()
	{
		Fps ();
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
