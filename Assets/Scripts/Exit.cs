using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (GameManager.instance.HasEnoughPoints())
        {
            Debug.Log("Player escaped!");

            GameManager.instance.WinLevel();
        }
        else
        {
            Debug.Log("Not enough points to escape!");

            // You could display a message here later.
        }
    }
}