using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CountdownTimer : MonoBehaviour {
	private bool timerActive = false;
	public float timeStart = 60;
	private float timeRemaining;
	bool isDestroyed = false; 

	public Text textDisplay;
    public Text score;
    static public int points;

    private static CountdownTimer instance;
	public GameObject congratsScreen;

	public GameObject timeOutScreen;
	
	void Awake() {
		// Implement Singleton
		if (instance == null) {
			instance = this;
		}
	}

    void Start() {
		timeOutScreen = GameObject.Find("UI/InGameUI/TimeOutScreen");
    	// Set initial variables
    	timeRemaining = timeStart;
        timerActive = true;
        points = 0;
    }

    void Update() {
        if (timerActive) {
        	if (timeRemaining > 0.0f) {
        		// Display current time
        		UpdateDisplay();

        		// Subtract time
        		timeRemaining -= Time.deltaTime;
        	} else {
				isDestroyed = true;
                // Timer has run out
        		Debug.Log("Time has run out!");
        		timeRemaining = 0;
        		timerActive = false;

				StartCoroutine(ShowTimeOutScreen());

				MonoBehaviour[] scripts = GetComponentsInChildren<MonoBehaviour>();

				foreach(MonoBehaviour script in scripts) {
					// Disable all weapons
					if(script is WeaponBase) {
						DisableWeapon((WeaponBase)script);
					}
					// Deactivate player controls
					else if(script is FirstPersonController) {
						DisableController((FirstPersonController)script);
					}
				}
				
                //Time.timeScale = 0f;

                //Scoreboard
                int i = 6;
                ScoreMenuScript.HighScoreArray[i] = points;
                while ((i >= 1) && (ScoreMenuScript.HighScoreArray[i] > ScoreMenuScript.HighScoreArray[i-1]))
                {
                    int t = ScoreMenuScript.HighScoreArray[i];
                    ScoreMenuScript.HighScoreArray[i] = ScoreMenuScript.HighScoreArray[i - 1];
                    ScoreMenuScript.HighScoreArray[i - 1] = t;
                    i--;
                }
                Debug.Log(points.ToString());
                score.text = points.ToString();
                congratsScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                PauseMenu.GamePaused = true;
        	}

            if (timeRemaining >= 6.00f && timeRemaining <= 7.00f) {
                FindObjectOfType<AudioManager>().Play("Siren");
            }
        }
    }

	void DisableWeapon(WeaponBase weapon) {
		weapon.IsEnabled = false;
	}

	void DisableController(FirstPersonController controller) {
		controller.enabled = false;
	}

	IEnumerator ShowTimeOutScreen() {
		timeOutScreen.SetActive(true);

		Image image = timeOutScreen.GetComponent<Image>();
		Color origColor = image.color;

		for (float alpha = 0.0f; alpha <= 1.1f; alpha += 0.1f) {
			image.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
			yield return new WaitForSeconds(0.1f);
		}

		yield break;
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

    void UpdateDisplay() {
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