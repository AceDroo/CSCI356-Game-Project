using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float turnSpeed = 4.0f;
    private float moveSpeed = 4.0f;
    private float sprintSpeed = 8.0f;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;
    private float rotY;

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
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // Clamp vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    }
    void KeyboardMovement() {
        // Control player movement
        if(Input.GetButton("Sprint")){
            mvX = Input.GetAxis("Horizontal") * Time.deltaTime * sprintSpeed;
            mvZ = Input.GetAxis("Vertical") * Time.deltaTime * sprintSpeed;
        }else{
            mvX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            mvZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        }
        
        if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0){
            mvX /= 2;
            mvY /= 2;
        }
    }
    void FixedUpdate() {
        //Move the player
        transform.Translate(new Vector3(mvX, 0, mvZ));
        //Rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + rotY, 0);
    }
}


// https://gamedevacademy.org/unity-3d-first-and-third-person-view-tutorial
// https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483