using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public enum Task
    {
        idle, move, follow, chase, attack
    }
    protected Task task = Task.idle;

    public GameObject selectBall;
    public GameObject GreenBall;
    public GameObject YellowBall;
    public GameObject RedBall;
    protected NavMeshAgent agent;
    protected Transform target;
    private Animator anim;

    public static List<ISelectable> SelectableUnits { get { return selectableUnits; } }
    static List<ISelectable> selectableUnits = new List<ISelectable>();

    //attack---------------------------------------------------------------------------
    public float attackDistance;
    //health---------------------------------------------------------------------------
    public float health;
    private float half;
    private float quater;
    private bool justOnce;

    //attack atomat
    List<Enemy> unitsList = new List<Enemy>();
    Enemy ClosesUnit
    {
        get
        {
            if (unitsList == null || unitsList.Count <= 0) return null;
            float minDistance = float.MaxValue;
            Enemy closesUnit = null;
            foreach (Enemy unit in unitsList)
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
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        half = health / 2;
        quater = health / 4;

        justOnce = true;
    }
    private void Start()
    {
        if (this is ISelectable)
        {
            selectableUnits.Add(this as ISelectable);
            (this as ISelectable).SetSelected(false);
        }

    }
    void Update()
    {
        healthStatus();
        switch (task)
        {
            case Task.idle: Idling(); break;
            case Task.move: Moving(); break;
            case Task.follow: Following();  break;
           // case Task.chase: Chasing();  break;
           // case Task.attack: Attacking(); break;
            default:
                break;
        }
    }
    protected virtual void Idling() 
    {
/*        agent.velocity = Vector3.zero;
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);*/
        attackAutomatically();
    }
    protected virtual void Moving() 
    {
        float distance = Vector3.Magnitude(agent.destination - transform.position);

        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);

        if (distance < 1) 
        {
            task = Task.idle;
        }
    }
    protected virtual void Following()
    {
        if (target)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance > attackDistance)
            {
                agent.SetDestination(target.transform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
            else if (distance <= attackDistance)
            {
                agent.velocity = Vector3.zero;
                transform.LookAt(target);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);
            }
        }
        else
        {
            attackAutomatically();
        }   
    }
    // protected virtual void Chasing() { }
    /*protected virtual void Attacking()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) <= attackDistance)
            {
                agent.velocity = Vector3.zero;
                transform.LookAt(target);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);
            }
            else
            {
                agent.SetDestination(target.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
        }
    }*/

    private void OnDestroy()
    {
        if (this is ISelectable) selectableUnits.Remove(this as ISelectable);
    }
    public void DeductHealth(float deductHealth)
    {
        health -= deductHealth;
        if (health <= 0)
        {
            playerDead();
        }
    }
    void healthStatus()
    {
        if (health > half)
        {
            GreenBall.SetActive(true);
            YellowBall.SetActive(false);
            RedBall.SetActive(false);
        }
        else if (health <= half && health >= quater)
        {
            GreenBall.SetActive(false);
            YellowBall.SetActive(true);
            RedBall.SetActive(false);
        }
        else if(health < quater && health > 0)
        {
            GreenBall.SetActive(false);
            YellowBall.SetActive(false);
            RedBall.SetActive(true);
        }
        else
        {
            GreenBall.SetActive(false);
            YellowBall.SetActive(false);
            RedBall.SetActive(false);
        }
    }
    void playerDead()
    {
        if (justOnce)
        {
            TextEnemyToKill.PlayerAmount -= 1;
            justOnce = false;
        }
        agent.velocity = Vector3.zero;
        anim.SetBool("Dead", true);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        Destroy(gameObject, 2f);
    }


    //automat attack
    private void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject.GetComponent<Enemy>();
        if (unit && !unitsList.Contains(unit))
        {
            unitsList.Add(unit);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var unit = other.gameObject.GetComponent<Enemy>();
        if (unit)
        {
            unitsList.Remove(unit);
        }
    }
    private void attackAutomatically()
    {
        var unit = ClosesUnit;
        if (unit)
        {
            if (Vector3.Distance(transform.position, unit.transform.position) <= 5 && Vector3.Distance(transform.position, unit.transform.position) > attackDistance)
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
            agent.velocity = Vector3.zero;
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
        }
    }

}//end
