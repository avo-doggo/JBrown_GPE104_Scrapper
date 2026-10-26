using UnityEngine;

public class ControllerAI : Controller
{

    private void Start()
    {

    }

    public override void MakeDecisions()
    {
        foreach (PawnAI enemy in GameManager.instance.Enemies)
        {
            if (enemy.isCrewmate && enemy.isAlerted) //allows enemy to seek
            {
                continue;
            }

            if (enemy != null)
            {
                if (enemy.movementX)
                {
                    if (enemy.movementRight)
                    {
                        enemy.moveRight();
                    }
                    else
                    {
                        enemy.moveLeft();
                    }

                }

                if (enemy.movementY == true)
                {
                    if (enemy.movementUp)
                    {
                        enemy.moveUp();
                    }
                    else
                    {
                        enemy.moveDown();
                    }
                }
            }
        }
    }


    private void FixedUpdate()
    {
        MakeDecisions();
    }

}
