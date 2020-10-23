using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {
	public GameObject player;
	public Transform spawnPoint;
	public GameObject enemySpawner;

	void Start() {
		GameObject playerObj = Instantiate(player, spawnPoint.position, spawnPoint.rotation);

		enemySpawner.SetActive(true);
		enemySpawner.GetComponent<EnemySpawner>().target = playerObj;
	}
}