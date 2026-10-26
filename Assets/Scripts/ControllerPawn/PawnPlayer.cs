using UnityEngine;

public class PawnPlayer : Pawn
{
    [Header("Player Stats")]
    public float movementSpeed;
    public Transform tf;
    public Rigidbody2D rb;

    [Header("Interaction")]
    public float interactRadius;

    [Header("Inventory")]
    public InventoryUI inventoryUI;

    public override void interact()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,interactRadius);

        foreach (Collider2D obj in objects)
        {
            Interactable interactable = obj.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }

    public override void Inventory()
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning("Inventory UI is not assigned!");
            return;
        }

        if (inventoryUI.inventoryPanel.activeSelf)
        {
            inventoryUI.CloseInventory();
        }
        else
        {
            inventoryUI.OpenInventory();
        }
    }

    public override void moveUp()
    {
        if (!inventoryUI.inventoryPanel.activeSelf)
        {
            tf.position += Vector3.up * Time.deltaTime * movementSpeed;
        }
    }

    public override void moveDown()
    {
        if (!inventoryUI.inventoryPanel.activeSelf)
        {
            tf.position += Vector3.down * Time.deltaTime * movementSpeed;
        }
    }

    public override void moveLeft()
    {
        if (!inventoryUI.inventoryPanel.activeSelf)
        {
            tf.position += Vector3.left * Time.deltaTime * movementSpeed;
        }
    }

    public override void moveRight()
    {
        if (!inventoryUI.inventoryPanel.activeSelf)
        { 
            tf.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }
}