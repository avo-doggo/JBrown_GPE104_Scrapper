using UnityEngine;
using UnityEngine.AI;

public class PawnAI : Pawn
{
    [Header("Enemy Stats")]
    public float movementSpeed;

    [Header("AI Type")]
    public bool isCrewmate;
    public bool isDrone;

    [Header("AI State")]
    public bool isAlerted;
    public bool investigating;
    public bool returning;
    public bool chasing;

    [Header("AI Positions")]
    public Vector3 investigatePosition;
    public Vector3 patrolPosition;

    public bool hasPatrolPosition;

    [Header("Target")]
    public Transform target;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    private NavMeshAgent agent;

    [Header("Alarm Sound")]
    public AudioSource audioSource;
    public AudioClip alarmSound;


    //INTERACT- currently not using

    public override void interact()
    {
        //Interact code for AI
    }


    //START

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError(gameObject.name + " does not have a NavMeshAgent!");
            return;
        }

        agent.speed = movementSpeed;

        // 2D NavMesh settings
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        patrolPosition = transform.position;
        hasPatrolPosition = true;

        if (!agent.isOnNavMesh)
        {
            Debug.LogError(gameObject.name + " STARTED OFF THE NAVMESH!");
        }
        else
        {
            Debug.Log(gameObject.name + " is correctly on the NavMesh.");
        }
    }

    //Update

    private void Update()
    {
        if (agent == null || !agent.isOnNavMesh)
        {
            return;
        }

        //CREWMATE
        if (isCrewmate)
        {
            if (chasing)
            {
                Seek(target);
            }
            else if (isAlerted)
            {
                if (investigating)
                {
                    Investigate();
                }
                else if (returning)
                {
                    ReturnToPatrol();
                }
            }
            else
            {
                Patrol();
            }
        }

        //DRONE
        if (isDrone)
        {
            Patrol();
        }
    }


    //NORMAL PATROL

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogWarning(gameObject.name + " has no patrol points!");
            return;
        }

        if (agent == null)
        {
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning(gameObject.name + " is not on the NavMesh!");
            return;
        }

        Transform patrolPoint = patrolPoints[currentPatrolPoint];

        agent.SetDestination(patrolPoint.position);

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }
    }


    //Look for the player

    public void Seek(Transform target)
    {
        if (target == null)
        {
            return;
        }

        agent.SetDestination(target.position);
    }

    public void StartChasing(Transform player)
    {
        target = player;

        chasing = true;
        investigating = false;
        returning = false;

        isAlerted = true;

        Debug.Log("Crewmate spotted the player! Chasing!");
    }



    //INVESTIGATE- search for the player  wherever the alert happened, then return to position of patrol

    void Investigate()
    {
        agent.SetDestination(investigatePosition);

        //Check if crewmate reached investigation location
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            investigating = false;
            returning = true;

            Debug.Log("Crewmate reached investigation location. Returning to patrol.");
        }
    }


    //RETURN TO PATROL - if the crewmate doesn't dfind the player

    void ReturnToPatrol()
    {
        agent.SetDestination(patrolPosition);

        //Check if crewmate returned
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            returning = false;
            isAlerted = false;
            target = null;

            Debug.Log("Crewmate returned to patrol.");
        }
    }


    //Drone collides with player then Alerts crew

    // Drone detects player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isDrone)
            {
                AlertCrew(collision.transform);
            }
        }
    }

    // Crewmate physically catches player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isCrewmate && chasing)
            {
                Debug.Log("CREWMATE CAUGHT PLAYER!");

                GameManager.instance.PlayerCaught();
            }
        }
    }


    //Alert the crewmates

    void AlertCrew(Transform player)
    {
        Debug.Log("Drone spotted player!");

        // Play alarm sound
        if (audioSource != null && alarmSound != null)
        {
            audioSource.PlayOneShot(alarmSound);
        }

        foreach (PawnAI enemy in GameManager.instance.Enemies)
        {
            if (enemy != null && enemy.isCrewmate)
            {
                enemy.BecomeAlerted(player, transform.position);
            }
        }
    }

    //Alerted!!

    public void BecomeAlerted(
        Transform player,
        Vector3 dronePosition)
    {
        //Remember where the crewmate was patrolling
        if (!hasPatrolPosition)
        {
            patrolPosition = transform.position;
            hasPatrolPosition = true;
        }

        isAlerted = true;
        investigating = true;
        returning = false;

        target = player;
        investigatePosition = dronePosition;

        Debug.Log("Crewmate investigating!");
    }


   
    //INVENTORY
//Not included
    public override void Inventory()
    {
        // Inventory Code
    }

    public override void moveUp()
    {
    }

    public override void moveDown()
    {
    }

    public override void moveLeft()
    {
    }

    public override void moveRight()
    {
    }
}