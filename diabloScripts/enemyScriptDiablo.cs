using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScriptDiablo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Rigidbody myBody;
    float normalSpeed;
    public float chasingSpeed;
    Transform player;

    //patrop
    float patrol_Timer;
    public float patrol_For_This_Time, patrol_Radius_Min, patrol_Radius_Max;

    float attackDistance = 1.5f;
    public GameObject attackPoint;

    public float MaxHealfh;
    float healfh;
    bool one = true;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();

        attackPoint.SetActive(false);

        normalSpeed = agent.speed;
        patrol_Timer = patrol_For_This_Time;

        healfh = MaxHealfh;
    }


    void Update()
    {
        if (healfh > 0)
        {
            movingSystem();
            WalkAndRun();
        }
        myHealth();
    }
    void movingSystem()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistanceScript.attackDistancePlus
            && Vector3.Distance(transform.position, player.transform.position) > attackDistance)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = chasingSpeed;
            anim.SetBool("Attack", false);
        }
        else if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            agent.velocity = Vector3.zero;
            transform.LookAt(player);
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
            agent.speed = normalSpeed;
            patrol_Timer += Time.deltaTime;

            if (patrol_Timer > patrol_For_This_Time)
            {
                SetNewRandomDestination();
                patrol_Timer = 0f;
            }
        }

    }
    void WalkAndRun()
    {
        if (agent.velocity.sqrMagnitude > 0)
        {
            if (agent.speed >4)
            {
                anim.SetFloat("Speed", 2);
            }
            else
            {
                anim.SetFloat("Speed", 1);
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "magicBall")
        {
            healfh -= 20;
        }
    }


    public void DeductHealth(float damage)
    {
        healfh -= damage;
    }
    void myHealth()
    {
        if (healfh <= 0)
        {
            gameObject.layer = 2;
            gameObject.tag = "Untagged";
            agent.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0);
            anim.SetBool("Attack", false);
            anim.SetBool("Dead", true);

            if (one)
            {
                barsScript.enemyToKill -= 1;
                one = false;
            }

            StartCoroutine(destroy());
            IEnumerator destroy()
            {
                yield return new WaitForSeconds(1f);
                Destroy(GetComponent<NavMeshAgent>());
                Destroy(GetComponent<enemyScriptDiablo>());
               // Destroy(GetComponent<Rigidbody>());
            }

        }
    }
    public void activeAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    public void disactiveAttackPoint()
    {
        attackPoint.SetActive(false);
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
}
