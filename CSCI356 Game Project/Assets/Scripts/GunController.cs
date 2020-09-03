using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	public Camera camera;
    public Transform gunEnd;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;
	private float timeToFire = 0f;

    void Start() {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire) {
        	timeToFire = Time.time + 1f  / fireRate;
            StartCoroutine(ShotEffect());
        	Shoot();
        }
    }
    void Shoot() {
    	// Initialise raycast hit
    	RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

    	// If object has been hit by raycast, deal damage
    	if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)) {
    		// Draw laser line
            laserLine.SetPosition(1, hit.point);

            // Display name of hit object
    		Debug.Log(hit.transform.name);

    		// If hit object has target component, deal damage
    		Target target = hit.transform.GetComponent<Target>();
    		if (target != null) {
    			target.TakeDamage(damage);
    		}
    	} else {
            laserLine.SetPosition(1, rayOrigin + (camera.transform.forward * range));
        }
    }
    private IEnumerator ShotEffect() {
        // gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}

// https://www.youtube.com/watch?v=THnivyG0Mvo <= Brackeys: Shooting with Raycasts - Unity Tutorial