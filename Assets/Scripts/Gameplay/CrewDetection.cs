using UnityEngine;

public class CrewDetection : MonoBehaviour
{
    public PawnAI crew;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (crew == null)
        {
            return;
        }

        // The crew only starts chasing if it is investigating.
        if (crew.isCrewmate && crew.isAlerted && crew.investigating)
        {
            crew.StartChasing(collision.transform);
        }
    }
}