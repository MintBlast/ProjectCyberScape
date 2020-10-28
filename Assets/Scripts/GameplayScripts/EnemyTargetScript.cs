using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetScript : MonoBehaviour
{
    //navigation mesh
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, IdentifyPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //health
    public float health = 100f;

    //attack time
    public float AttackTime = 2f;

    public bool CanAttack = true;

    //damage
    public float damage = 25f;

    //private PlayerHealth playerHealth;

    

    private void Awake()
    {
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //playerHealth = player.GameObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, IdentifyPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, IdentifyPlayer);
        

        //if player is not spotted and not in range
        if (!playerInSightRange && !playerInAttackRange)
        {
            //patrol
            Patrolling();
        }

        //if player is spotted and not in range
        if (playerInSightRange && !playerInAttackRange)
        {
            //chase player
            ChasePlayer();
        }

        //if player is spotted and in range
        if (!playerInSightRange && playerInAttackRange)
        {
            //attack
            AttackPlayer();
        }
    }

    private void Patrolling()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distancetoWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distancetoWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range in Z axis
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        //Calculate random point in range in X axis
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, IdentifyPlayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        //
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //ShootPlayer();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {
        //amount of damage taken 
        health -= amount;

        //when hp is 0
        if (health <= 0f)
        {
            //dies
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Down");
    }

    /*void ShootPlayer()
    {
        RaycastHit Hit;

        if(Physics.Raycast(transform.position, transform.forward, out Hit))
        {
            playerHealth = Hit.transform.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }*/
}

