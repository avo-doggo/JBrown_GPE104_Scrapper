using UnityEngine;


public class ControllerPlayer : Controller
{
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode interactKey;
    public KeyCode inventoryKey;

    public override void MakeDecisions()
    {
        if (pawn != null)
        {
            if (Input.GetKey(moveUpKey))
            {
                pawn.moveUp();
            }
            if (Input.GetKey(moveDownKey))
            { 
                pawn.moveDown(); 
            }
            if (Input.GetKey(moveLeftKey))
            { 
                pawn.moveLeft(); 
            }
            if (Input.GetKey(moveRightKey))
            {
                pawn.moveRight();
            }
            if (Input.GetKeyDown(interactKey))
            {
                pawn.interact();
            }
            if (Input.GetKeyDown(inventoryKey))
            {
                pawn.Inventory();
            }

        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MakeDecisions();
    }
}
