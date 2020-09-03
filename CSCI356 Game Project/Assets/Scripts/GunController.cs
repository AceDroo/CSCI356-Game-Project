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
    	// Initialise raycast hit
    	RaycastHit hit;

    	// If object has been hit by raycast, deal damage
    	if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)) {
    		// Display name of hit object
    		Debug.Log(hit.transform.name);

    		// If hit object has target component, deal damage
    		Target target = hit.transform.GetComponent<Target>();
    		if (target != null) {
    			target.TakeDamage(damage);
    		}
    	}
    }
}
