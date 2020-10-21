using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	public GameObject player;
	public Transform spawnPoint;
	public GameObject enemySpawner;
	public GameObject lobbyCam;
	public GameObject lobbyUI;
	public GameObject inGameUI;

	void Start() {
		lobbyCam.SetActive(false);
		lobbyUI.SetActive(false);

		GameObject playerObj = Instantiate(player, spawnPoint.position, spawnPoint.rotation);

		inGameUI.SetActive(true);
		enemySpawner.SetActive(true);
		enemySpawner.GetComponent<EnemySpawner>().target = playerObj;
	}
}