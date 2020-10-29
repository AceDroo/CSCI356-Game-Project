using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : HealthManager
{
	// Object to instantiate upon dying
    public GameObject destroyedVersion;

    // Visual component for taking damage
    public float shakeDelay = 0.002f;
    private float shakeIntensity = 0.03f;
    private float tempShakeIntensity = 0;
    private bool shaking = false;

     // Navigation settings
    Animator animator;
    NavMeshAgent agent;

    public GameObject target;
    public float damage = 15.0f;
    public float chaseDistance = 20.0f;
    public float attackDistance = 2.0f;
    public bool isAttacking = false;
    public bool shouldChase = true;

    private float distance;
    private Vector3 targetPos;
    private Vector3 originalPos;
    private Quaternion originalRot;

    private float origSpeed;

    void Start()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        // Check if dead
        if (!IsDead) 
        {
            if (shaking) {
                //Calculate both linear and rotational displacement
                Vector3 movement = new Vector3(originalPos.x ,originalPos.y,originalPos.z + (Random.Range(-shakeIntensity, shakeIntensity) ));
                Vector3 rotation = new Vector3(originalRot.x, originalRot.y + (1 + Random.Range(-shakeIntensity, 1 + shakeIntensity)), originalRot.z  );

                //Displace
                transform.position = movement;
                transform.Rotate(rotation);

                //Shake gets smaller as it continues
                tempShakeIntensity -= shakeDelay;
            }

            if (!isAttacking) 
            {
                // Calculate distance between player and enemy
                float distance = (target.transform.position - transform.position).sqrMagnitude;

                // Check if within range
                if (distance <= chaseDistance)
                {
                    if (agent.path.status == NavMeshPathStatus.PathComplete)
                    {
                        // Get course-corrected target position
                        targetPos = target.transform.position + agent.velocity * Time.deltaTime;

                        // Set the destination to the target position
                        agent.SetDestination(targetPos);

                        // Turn to face player
                        transform.LookAt(target.transform);

                        // Enter walk mode
                        animator.SetTrigger("Walk");
                    }
                    else
                    {
                        agent.isStopped = true;
                    }
                }
            }

            // If no path is found, return
            if (agent.pathPending) return;

            // Check if can attack
            CheckAttack(); 
        }
    }

    void CheckAttack()
    {
        // Calculate actual distance from target
        float distanceFromTarget = (target.transform.position - transform.position).sqrMagnitude;

        // Calculate direction is toward player
        Vector3 direction = target.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (!isAttacking && distanceFromTarget <= attackDistance && angle <= 60f) 
        {
            // Enter attack state
            isAttacking = true;
            shouldChase = false;

            // Update speed
            origSpeed = agent.speed;
            agent.speed = 0;

            // Enter scratch animation
            animator.SetTrigger("Scratch");

            // Get player controller and apply damage
            PlayerController player = target.GetComponent<PlayerController>();
            
            if (player)
            {
                player.ApplyDamage(damage);
            }

            // Reset attack
            StartCoroutine("ResetAttacking");
        }
    }

    IEnumerator ResetAttacking() 
    {
        yield return new WaitForSeconds(1.4f);

        isAttacking = false;
        shouldChase = true;

        if(!IsDead) {
            agent.speed = origSpeed;
        }
        
        yield break;
    }

    public override void ApplyDamage(float damage) {
        // Check if entity is already dead
        if (IsDead) return;

        // Perform shake
        Shake();

        // Otherwise, apply damage
        health -= damage;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        // Alterations in spawn location due to object center missalignments
        Quaternion rotationAmount = Quaternion.Euler(-90, 0, 0);
        Vector3 positionAmount = new Vector3(0, 2, 0);

        //Instantiate the severed partss
        GameObject bodyParts = Instantiate(destroyedVersion, transform.position + positionAmount, transform.rotation * rotationAmount);
        
        //Re-scale due to object missalignment
        float scale = 10.0f;
        bodyParts.transform.localScale = transform.localScale * scale;

        //Destroy remaining game objects
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);

        // Increase score
        ScoreManager.points += 500;
        
    }

    // Visual representation of the target losing health as a result of taking damage
    void Shake() 
    {
        // Reset max displacement of "shake" feature
        tempShakeIntensity = shakeIntensity;

        // Commence coroutine
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow() 
    {
        // Record pre-shake position
        originalPos = transform.position;   // Modify with transform.parent.position if need be
        originalRot = transform.rotation;   // Modify with transform.parent.rotation if need be
        shaking = true;

        ///How long the target will be sh00k
        yield return new WaitForSeconds(.3f);

        //Return target to pre-shake position
        shaking = false;
        transform.position = originalPos;       // Modify with transform.parent.position if need be
        transform.rotation = originalRot;       // Modify with transform.parent.rotation if need be
    }
}
