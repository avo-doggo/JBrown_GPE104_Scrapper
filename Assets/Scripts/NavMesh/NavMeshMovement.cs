using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    [SerializeField] public Transform targetPositionTransform;


    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent <NavMeshAgent>();
    }

    private void Update()
    {
       // navMeshAgent.destination =
    }
}
