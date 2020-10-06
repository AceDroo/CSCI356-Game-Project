using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {
	private bool timerActive = false;
	public float timeStart = 60;
	private float timeRemaining;

	public Text textDisplay;

	private static CountdownTimer instance;

	void Awake() {
		// Implement Singleton
		if (instance == null) {
			instance = this;
		}
	}

    void Start() {
    	// Set initial variables
    	timeRemaining = timeStart;
        timerActive = true;
    }

    void Update() {
        if (timerActive) {
        	if (timeRemaining > 0) {
        		// Display current time
        		UpdateDisplay();

        		// Subtract time
        		timeRemaining -= Time.deltaTime;
        	} else {
                // Timer has run out
        		Debug.Log("Time has run out!");
        		timeRemaining = 0;
        		timerActive = false;
        	}
        }
    }

    public void PauseTimer() {
    	timerActive = !timerActive;
    }

    public void AddTime(float time) {
    	timeRemaining += time;
    }

    public void RemoveTime(float time) {
    	if (timeRemaining - time > 0) {
    		timeRemaining -= time;
    	}
    }

    void UpdateDisplay(float timeToDisplay) {
    	float minutes = Mathf.FloorToInt(timeRemaining / 60);
    	float seconds = Mathf.FloorToInt(timeRemaining % 60);

    	textDisplay.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }
}

/// 19/09/2020
/// https://www.youtube.com/watch?v=Qhm_t46kuM4
/// https://www.youtube.com/watch?v=qc7J0iei3BU
/// https://www.youtube.com/watch?v=o0j7PdU88a4

/// 06/10/2020
/// https://answers.unity.com/questions/225213/c-countdown-timer.html
/// https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/