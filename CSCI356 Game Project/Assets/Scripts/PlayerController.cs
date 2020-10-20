using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerStats stats;
    public float moveSpeed;
    private int sprintBar = 100;
    private float rotX;
    private float rotY;
    private float mvX = 0;
    private float mvY = 0;
    private float mvZ = 0;
    private bool crouch = false;
    private Transform gun;
    private float gunScaleY;
    private Rigidbody rigidBody;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        gun = this.transform.GetChild(1);
        rigidBody = GetComponent<Rigidbody>();
        Vector3 gunScale = gun.transform.localScale;
        gunScaleY = gunScale.y;
    }
    void Update() {
        MouseAiming();
        KeyboardMovement();
    }
    void MouseAiming() {
        // Get mouse inputs
        rotY = Input.GetAxis("Mouse X") * stats.turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * stats.turnSpeed;

        // Clamp vertical rotation
        rotX = Mathf.Clamp(rotX, stats.minTurnAngle, stats.maxTurnAngle);
    }
    void KeyboardMovement() {
        //Set movement speed
        if(Input.GetButton("Sprint") && !crouch && sprintBar > 0) {
            moveSpeed = stats.sprintSpeed;
        }else if (crouch){
            moveSpeed = stats.crouchSpeed;
        }else{
            moveSpeed = stats.walkSpeed;
        }

        //Crouch/Un-crouch
        if(Input.GetButtonDown("Crouch")){
            crouch = true;
            transform.localScale += new Vector3(0,-0.5f,0);
            gun.localScale += new Vector3(0,gunScaleY,0);
            transform.localPosition += new Vector3(0,-0.5f,0);
        }
        if(Input.GetButtonUp("Crouch")){
            crouch = false;
            transform.localPosition += new Vector3(0,0.5f,0);
            transform.localScale += new Vector3(0,0.5f,0);
            gun.localScale += new Vector3(0,-gunScaleY,0);
        }
        //Jump when the player is grounded
        if(Input.GetButtonDown("Jump")){
            RaycastHit hit;
            if(Physics.Raycast(transform.position + new Vector3(1,0,0), -transform.up, out hit, 1.1f) ||
            Physics.Raycast(transform.position + new Vector3(-1,0,0), -transform.up, out hit, 1.1f) ||
            Physics.Raycast(transform.position + new Vector3(0,0,1), -transform.up, out hit, 1.1f) ||
            Physics.Raycast(transform.position + new Vector3(0,0,-1), -transform.up, out hit, 1.1f)){
                rigidBody.AddForce(new Vector3(0, stats.jumpForce,0), ForceMode.Impulse);
            }
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
        // Move the player
        transform.Translate(new Vector3(mvX, 0, mvZ));

        // Rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + rotY, 0);

        // Fill the sprint bar
        if(!Input.GetButton("Sprint")){
            if (sprintBar < 100){
                sprintBar++;
            }
        }else{
            if (sprintBar > 0){
                sprintBar--;
            }
        }
    }
}