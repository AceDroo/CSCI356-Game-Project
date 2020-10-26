using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupManager : MonoBehaviour {
	public Text text;
	public AudioManager audioManager;
	public WorldManager worldManager;
	public GameObject timer;
	public GameObject healthUI;
	public GameObject staminaUI;
	public AudioSource music;
	private PlayerController player;
	int remainSeconds;

	void Start() {
		audioManager.Play("Prepare");
		player = FindObjectsOfType<PlayerController>()[0];

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
		healthUI.SetActive(true);
		staminaUI.SetActive(true);
		music.Play();

		//player = worldManager.GetComponent<WorldManager>().GetPlayer().GetComponent<PlayerController>();

		player.SetPlayerControl();

		yield return new WaitForSeconds(3);
		Destroy(text);
	}

	void UpdateText(int sec) {
		text.text = "Begins in " + sec + " seconds.";
	}
}