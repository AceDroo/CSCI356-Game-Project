using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float turnSpeed = 4.0f;
    private float moveSpeed = 2.0f;

    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;

    private float mvX = 0;
    private float mvY = 0;
    private float mvZ = 0;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        MouseAiming();
        KeyboardMovement();
    }
    void MouseAiming() {
        // Get mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // Clamp vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        // Rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
    void KeyboardMovement() {
        // Control player movement
        mvX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        mvZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(new Vector3(mvX, 0, mvZ));
    }
}


// https://gamedevacademy.org/unity-3d-first-and-third-person-view-tutorial
// https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483