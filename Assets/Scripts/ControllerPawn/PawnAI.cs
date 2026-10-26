using UnityEngine;

public class PawnAI : Pawn
{
    [Header("Enemy Stats")]
    //what iks enemys movement speed
    public float movementSpeed;
    //is enemy moviung on X axis
    public bool movementX;
    //is enemy moving on Y axis
    public bool movementY;
    //Is enemy moving up
    public bool movementUp;
    //is enemy moving right
    public bool movementRight;

    public bool isCrewmate;
    public bool isDrone;

    public bool isAlerted;
    public bool investigating;

    public Vector3 investigatePosition;
    public Vector3 patrolPosition;
    public bool hasPatrolPosition;
    public bool returning;

    public Transform target;

    public Rigidbody2D rb;



    public override void interact()
    {
        //Interact code for AI
    }

    public override void moveUp()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * movementSpeed;
    }
    public override void moveDown()
    {
        transform.position = transform.position + -Vector3.up * Time.deltaTime * movementSpeed;
    }

    public override void moveLeft()
    {
        transform.position = transform.position + -Vector3.right * Time.deltaTime * movementSpeed;
    }

    public override void moveRight()
    {
        transform.position = transform.position + Vector3.right * Time.deltaTime * movementSpeed;
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {//on collision with objects tagged wall, the  enemy will change direction
        if (collision.gameObject.CompareTag("Wall"))
        {
            if(movementX)
            {
                movementRight = !movementRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            if (movementY)
            {
                movementUp = !movementUp;
            }
        }

        if (isDrone && collision.gameObject.CompareTag("Player"))
        {
            AlertCrew(collision.transform);
        }
    }

    void AlertCrew(Transform player)
    {
        Debug.Log("Drone spotted player!");

        foreach (PawnAI enemy in GameManager.instance.Enemies)
        {
            if (enemy != null && enemy.isCrewmate)
            {
                enemy.BecomeAlerted(player, transform.position);
            }
        }
    }
    //This happens when the crewmate is alerted
    public void BecomeAlerted(Transform player, Vector3 dronePosition)
    {
        if (!hasPatrolPosition)
        {
            patrolPosition = transform.position;
            hasPatrolPosition = true;
        }

        isAlerted = true;
        investigating = true;

        target = player;
        investigatePosition = dronePosition;

        Debug.Log("Crewmate investigating!");
    }

    public void Seek(Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    void Investigate()
    {
        Vector3 direction = (investigatePosition - transform.position).normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(
            transform.position,
            investigatePosition
        );

        if (distance < 0.1f)
        {
            investigating = false;
            returning = true;

            Debug.Log("Crewmate reached investigation location. Returning to patrol.");
        }
    }

    void ReturnToPatrol()
    {
        Vector3 direction = (patrolPosition - transform.position).normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(
            transform.position,
            patrolPosition
        );

        if (distance < 0.1f)
        {
            returning = false;
            isAlerted = false;
            target = null;

            Debug.Log("Crewmate returned to patrol.");
        }
    }

    //START
    void Start()
    {
        //get the rigid body of the attached component
        rb = GetComponent<Rigidbody2D>();
    }

    //UPDATE
    void FixedUpdate()
    {
        if (isCrewmate && isAlerted) //goes to location player triggered alert
        {
            if (investigating) //looks for player
            {
                Investigate();
            }
            else if (returning) //returns to original position
            {
                ReturnToPatrol();
            }
            else if (target != null)
            {
                Seek(target);
            }

            return;
        }

        // Normal patrol
        if (movementX)
        {
            if (movementRight)
                moveRight();
            else
                moveLeft();
        }

        if (movementY)
        {
            if (movementUp)
                moveUp();
            else
                moveDown();
        }
    }

    public override void Inventory()
    {
        //Inventory Code
    }
}
