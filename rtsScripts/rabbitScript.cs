using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RabbitState
{
    PATROL,

}
public class rabbitScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private RabbitState rabbitState;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        rabbitState = RabbitState.PATROL;
        patrol_Timer = patrol_For_This_Time;
    }

    void Update()
    {
        if (rabbitState == RabbitState.PATROL)
        {
            Patrol();
        }


    }
    void Patrol()
    {

        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void SetNewRandomDestination()
    {

        float rand_Radius = UnityEngine.Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = UnityEngine.Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        agent.SetDestination(navHit.position);

    }

}//end
