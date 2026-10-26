using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public abstract void moveUp();
    public abstract void moveDown();
    public abstract void moveLeft();
    public abstract void moveRight();
    public abstract void interact();

    public abstract void Inventory();
}
