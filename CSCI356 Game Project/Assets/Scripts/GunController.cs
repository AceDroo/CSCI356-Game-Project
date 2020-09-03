using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
	public float damage = 10f;
	public float range = 100f;

	public Camera camera;

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
        	Shoot();
        }
    }
    void Shoot() {
    	RaycastHit hit;
    	if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)) {
    		Debug.Log(hit.transform.name);
    	}
    }
}
