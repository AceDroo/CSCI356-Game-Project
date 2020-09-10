using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float turnSpeed = 4.0f;
    private float moveSpeed = 4.0f;

    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;
    private float rotY;

    private float mvX = 0;
    private float mvY = 0;
    private float mvZ = 0;
    private Vector3 moveVector;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        MouseAiming();
        KeyboardMovement();
    }
    void Jump(){

    }
    void MouseAiming() {
        // Get mouse inputs
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // Clamp vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        
        
    }
    void KeyboardMovement() {
        // Control player movement
        mvX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        mvZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        moveVector = new Vector3(-rotX, transform.eulerAngles.y + rotY, 0);
        moveVector.Normalize();
    }
    void FixedUpdate() {
        //Move the player
        transform.Translate(new Vector3(mvX, 0, mvZ));
        //Rotate the camera
        
        transform.eulerAngles = moveVector;
    }
}


// https://gamedevacademy.org/unity-3d-first-and-third-person-view-tutorial
// https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483