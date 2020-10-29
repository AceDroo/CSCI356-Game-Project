using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {
	public Transform spawnPoint;
	public GameObject enemySpawner;

	[SerializeField] List<GameObject> players = new List<GameObject>();
	public GameObject player;
	public Text startupText;
	public AudioManager audioManager;
	public GameObject timerUI;
	public GameObject healthUI;
	public GameObject staminaUI;
	public AudioSource music;
	public GameObject gameUI;

	public int remainSeconds;

	private GameObject playerObj;

	void Start() {
		// Initialise the player
		playerObj = Instantiate(player, spawnPoint.position, spawnPoint.rotation);

		audioManager.Play("Prepare");

		//enemySpawner.SetActive(true);
		//enemySpawner.GetComponent<EnemySpawner>().target = playerObj;

		// Begin start countdown
		StartCoroutine(StartAnimation());
		gameUI.SetActive(true);
		enemySpawner.SetActive(true);
		enemySpawner.GetComponent<EnemySpawner>().target = playerObj;
	}

	IEnumerator StartAnimation() {
		for(int i = remainSeconds; i > 0; i--) {
			if (i <= 5) {
				audioManager.Play("Beep");
			}

			UpdateStartUpText(i);
			yield return new WaitForSeconds(1f);
		}

		BeginGame();

		yield return new WaitForSeconds(3);
		
		Destroy(startupText);
	}

	public void BeginGame() {
		// Update startup text
		startupText.text = "FIRE!";

		// Play alarm
		audioManager.Play("Warning");

		// Set the UI to be active
		timerUI.SetActive(true);
		healthUI.SetActive(true);
		staminaUI.SetActive(true);

		// Activate the player and put them in control
		playerObj.GetComponent<PlayerController>().SetPlayerControl();

		// Play music
		music.Play();
	}

	public GameObject GetPlayer() {
		return playerObj;
	}

	void UpdateStartUpText(int sec) {
		startupText.text = "Begins in " + sec + " seconds.";
	}
}