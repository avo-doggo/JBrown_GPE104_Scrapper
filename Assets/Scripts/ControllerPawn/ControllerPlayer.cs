using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    [Header("Keyboard Controls")]
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode interactKey;
    public KeyCode inventoryKey;

    [Header("Controller")]
    public float controllerDeadzone = 0.2f;

    public override void MakeDecisions()
    {
        if (pawn == null)
        {
            return;
        }

        //KEYBOARD MOVEMENT

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

        // Keyboard interact
        if (Input.GetKeyDown(interactKey))
        {
            pawn.interact();
        }

        // Keyboard inventory
        if (Input.GetKeyDown(inventoryKey))
        {
            pawn.Inventory();
        }


        //CONTROLLER

        if (Gamepad.current != null)
        {
            Vector2 stickInput =
                Gamepad.current.leftStick.ReadValue();

            // Move up
            if (stickInput.y > controllerDeadzone)
            {
                pawn.moveUp();
            }

            // Move down
            if (stickInput.y < -controllerDeadzone)
            {
                pawn.moveDown();
            }

            // Move left
            if (stickInput.x < -controllerDeadzone)
            {
                pawn.moveLeft();
            }

            // Move right
            if (stickInput.x > controllerDeadzone)
            {
                pawn.moveRight();
            }

            // A button = interact
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                pawn.interact();
            }

            // Y button = inventory
            if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                pawn.Inventory();
            }
        }
    }

    private void Update()
    {
        MakeDecisions();
    }
}