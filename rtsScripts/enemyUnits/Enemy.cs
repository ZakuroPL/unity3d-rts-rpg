using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //public GameObject basePlayer;
    private NavMeshAgent agent;
    private Transform newTarget;
    private Animator anim;
    public bool IsAttackingBase;
    public GameObject baseToAttack;

    public float health;
    private bool justOnce;

    List<Unit> unitsList = new List<Unit>();
    Unit ClosesUnit
    {
        get
        {
            if (unitsList == null || unitsList.Count <= 0) return null;
            float minDistance = float.MaxValue;
            Unit closesUnit = null;
            foreach(Unit unit in unitsList)
            {
                if (!unit) continue;
                float distance = Vector3.Magnitude(unit.transform.position - transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closesUnit = unit;
                }
            }
            return closesUnit;
        }
    }

    public float attackDistance;
    public GameObject attackObject;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attackObject.SetActive(false);
        justOnce = true;

    }
    void Start()
    {
        //newTarget = null;
    }


    void Update()
    {
        moveToTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject.GetComponent<Unit>();
        if (unit && !unitsList.Contains(unit))
        {
            unitsList.Add(unit);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var unit = other.gameObject.GetComponent<Unit>();
        if (unit)
        {
            unitsList.Remove(unit);
        }
    }
    void moveToTarget()
    {
        var unit = ClosesUnit;
        if (unit)
        {
            if (Vector3.Distance(transform.position, unit.transform.position) <= 25 && Vector3.Distance(transform.position, unit.transform.position) > attackDistance)
            {
                agent.SetDestination(unit.transform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
            else if (Vector3.Distance(transform.position, unit.transform.position) <= attackDistance)
            {
                agent.velocity = Vector3.zero;
                transform.LookAt(unit.transform.position);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);
            }
            else
            {
                agent.velocity = Vector3.zero;
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            if (IsAttackingBase)
            {
                agent.SetDestination(baseToAttack.transform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
            else
            {
                agent.velocity = Vector3.zero;
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
            }

        }
       
    }
    //damage from player
    public void DeductHealth(float deductHealth)
    {
        health -= deductHealth;
        if (health <= 0)
        {
            EnemyDead();
        }
    }
    void activeAttackScript()
    {
        attackObject.SetActive(true);
        StartCoroutine(disactive());
        IEnumerator disactive()
        {
            yield return new WaitForSeconds(0.5f * Time.deltaTime);
            attackObject.SetActive(false);
        }
    }
    void EnemyDead()
    {
        if (justOnce)
        {
            TextEnemyToKill.EnemyAmount -= 1;
            justOnce = false;
        }
        agent.velocity = Vector3.zero;
        anim.SetBool("Dead", true);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        Destroy(gameObject, 2f);
    }






}//end












/*



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //public GameObject basePlayer;
    public GameObject[] PlayerUnits;
    private NavMeshAgent agent;
    private Transform newTarget;
    private Animator anim;

    public float health;

    public float attackDistance;
    public GameObject attackObject;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attackObject.SetActive(false);

    }
    void Start()
    {
        //newTarget = null;
    }


    void Update()
    {
        moveToTarget();
    }
    void moveToTarget()
    {
        PlayerUnits = GameObject.FindGameObjectsWithTag("Player");
        float minDist = Mathf.Infinity;
        foreach (GameObject oneUnit in PlayerUnits)
        {
            float distance = Vector3.Distance(oneUnit.gameObject.transform.position, transform.position);
            if (distance < minDist)
            {
                newTarget = oneUnit.transform;
                minDist = distance;
            }
            if (Vector3.Distance(transform.position, newTarget.position) <= 20 && Vector3.Distance(transform.position, newTarget.position) > attackDistance)
            {
                agent.SetDestination(newTarget.transform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
            else if (Vector3.Distance(transform.position, newTarget.position) <= attackDistance)
            {
                agent.velocity = Vector3.zero;
                transform.LookAt(oneUnit.transform.position);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);
            }
            else
            {
                agent.velocity = Vector3.zero;
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
            }

        }
    }
    //damage from player
    public void DeductHealth(float deductHealth)
    {
        health -= deductHealth;
        if (health <= 0)
        {
            EnemyDead();
        }
    }
    void activeAttackScript()
    {
        attackObject.SetActive(true);
        StartCoroutine(disactive());
        IEnumerator disactive()
        {
            yield return new WaitForSeconds(0.1f);
            attackObject.SetActive(false);
        }
    }
    void EnemyDead()
    {
        Destroy(gameObject, 2f);
    }
}
*/