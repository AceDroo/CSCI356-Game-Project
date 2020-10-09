using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {
	public Text textDisplay;
	public float startTime;

	private float currentTime;
	private bool timerActive = true;

	private static CountdownTimer instance;
	public GameObject congratsScreen;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	void Start() {
		// Set timer
		currentTime = startTime;

		// Begin Timer
		StartCoroutine(UpdateTimer());
	}

	/*
	void Update() {
		if (timerActive == false && secondsLeft > 0) {
			StartCoroutine(UpdateTimer());
		}
	}

	IEnumerator UpdateTimer() {
		timerActive = true;
    	yield return new WaitForSeconds(1);
    	secondsLeft -= 1;
    	UpdateTimerText();
    	timerActive = false;
	}
	
	private void UpdateTimerText() {
    	if (secondsLeft < 10) {
    		textDisplay.text = "Time Left: 00:00:0" + secondsLeft;
    	} else {
    		textDisplay.text = "Time Left: 00:00:" + secondsLeft;
    	}
    }

    */

	private IEnumerator UpdateTimer() {
		while (timerActive) {
			// Decrement time
			currentTime -= 1 * Time.deltaTime;

			// If current time is less than or equal to zero, stop
			if (currentTime <= 0.0f) {
				currentTime = 0.0f;
				Time.timeScale = 0f;
				congratsScreen.SetActive(true);
				Cursor.lockState = CursorLockMode.Confined;
				PauseMenu.GamePaused = true;
			}

			if(currentTime >= 6.00f && currentTime <= 7.00f)
            {
				FindObjectOfType<AudioManager>().Play("Siren");
            }

			// Update Text Display
			if (currentTime < 10) {
				textDisplay.text = "Time Left: 0" + currentTime.ToString("#.00");
			} else {
				textDisplay.text = "Time Left: " + currentTime.ToString("#.00");
			}

			yield return null;
		}
	}

	public void AddTime(float amount) {
		currentTime += amount;
	}
}

/// 19/09/2020
/// https://www.youtube.com/watch?v=Qhm_t46kuM4
/// https://www.youtube.com/watch?v=qc7J0iei3BU
/// https://www.youtube.com/watch?v=o0j7PdU88a4