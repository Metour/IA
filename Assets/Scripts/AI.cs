using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Estados IA, para anadir un nuevo estado poner una coma.
    enum State
    {
        Patrolling,
        Chasing
    }

    State currentState;

    NavMeshAgent agent;

    public Transform[] destinationPoints;
    int destinationIndex = 0;

    public Transform player;

    [SerializeField]
    float visionRange;
    
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrolling;

        destinationIndex = Random.Range(0, destinationPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Con el TAB se hace el espacio del Patrol y el Chase.
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;

            default:
                Chase();
            break;
        }
    }

    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 1)
        {
            destinationIndex = Random.Range(0, destinationPoints.Length);
        }

        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

    void Chase()
    {
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }
    }

    void OnDrawGizmos()
    {
        foreach (Transform point in destinationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
