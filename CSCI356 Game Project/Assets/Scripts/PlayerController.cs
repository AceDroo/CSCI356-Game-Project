using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float turnSpeed = 4.0f;
    public float jumpForce = 6.0f;
    private float moveSpeed;
    public float sprintSpeed = 8.0f;
    public float walkSpeed = 4.0f;
    public float crouchSpeed = 2.0f;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;
    private float rotY;
    private float mvX = 0;
    private float mvY = 0;
    private float mvZ = 0;
    private bool crouch = false;
    private Transform gun;
    private bool grounded;
    private Rigidbody rigidBody;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        gun = this.transform.GetChild(1);
        rigidBody = GetComponent<Rigidbody>();
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
        //Set movement speed
        if(Input.GetButton("Sprint") && !crouch){
            moveSpeed = sprintSpeed;
        }else if (crouch){
            moveSpeed = crouchSpeed;
        }else{
            moveSpeed = walkSpeed;
        }

        //Crouch/Un-crouch
        if(Input.GetButtonDown("Crouch")){
            crouch = true;
            transform.localScale += new Vector3(0,-0.5f,0);
            gun.localScale += new Vector3(0,0.25f,0);
            transform.localPosition += new Vector3(0,-0.5f,0);
        }
        if(Input.GetButtonUp("Crouch")){
            crouch = false;
            transform.localPosition += new Vector3(0,0.5f,0);
            transform.localScale += new Vector3(0,0.5f,0);
            gun.localScale += new Vector3(0,-0.25f,0);
        }

        if(grounded && Input.GetButtonDown("Jump")){
            grounded = false;
            rigidBody.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
        }

        //Set move vectors
        mvX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        mvZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //Fix diagonal movement speed
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
    void OnCollisionStay(){
        grounded = true;
    }
}


// https://gamedevacademy.org/unity-3d-first-and-third-person-view-tutorial
// https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483