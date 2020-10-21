﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupText : MonoBehaviour {
	public Text text;
	public AudioManager audioManager;
	public GameObject timer;
	public AudioSource music;
	int remainSeconds;

	void Start() {
		audioManager.Play("Prepare");

		remainSeconds = 10;

		StartCoroutine(StartAnimation());
	}

	IEnumerator StartAnimation() {
		for(int i = remainSeconds; i > 0; i--) {
			if (i <= 5) {
				audioManager.Play("Beep");
			}

			UpdateText(i);
			yield return new WaitForSeconds(1f);
		}

		text.text = "FIRE!";
		audioManager.Play("Warning");
		timer.SetActive(true);
		music.Play();

		yield return new WaitForSeconds(3);
		Destroy(text);
	}

	void UpdateText(int sec) {
		text.text = "Begins in " + sec + " seconds.";
	}
}