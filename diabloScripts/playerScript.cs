using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class playerScript : MonoBehaviour
{
    Camera cam;
    public LayerMask clickMask;
    NavMeshAgent agent;
    Animator anim;

    GameObject EnemyPoint;
    Transform followEnemy;
    public static bool attackingMode;
    public GameObject attackPoint;

    public float MaxHealfh;
    float healfh;
    public static float healthProcent;

    public static float maxEnergy = 100;
    public static float  energy;

    public static float maxMana = 100;
    public static float mana;
    public GameObject magicBall;
    public Transform firePoint;

    void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        attackPoint.SetActive(false);

        healfh = MaxHealfh;
        energy = maxEnergy;
        mana = maxMana;
        healthProcent = healfh / MaxHealfh;
    }
    void Update()
    {
        moving();
        attack();
        animationControl();
        magic();

        healthProcent = healfh / MaxHealfh;

        if (energy <= 0)
        {
            energy = 0;
        }
        else if (energy >= maxEnergy) energy = maxEnergy;
    }

    void moving()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, clickMask))
            {
                if(hit.collider is TerrainCollider)
                {
                    agent.SetDestination(hit.point);
                    EnemyPoint = null;
                    followEnemy = null;

                }
                else if(hit.collider.tag == "Enemy")
                {
                    EnemyPoint = hit.collider.gameObject;
                    followEnemy = EnemyPoint.transform;
                    if (EnemyPoint)
                    {
                        agent.SetDestination(followEnemy.position);
                    }
                    else EnemyPoint = null;
                }
            }
        }
    }
    void attack()
    {
        if (followEnemy)
        {
            if (Vector3.Distance(transform.position, followEnemy.transform.position) <= 1.5)
            {
                attackingMode = true;
                agent.velocity = Vector3.zero;
                transform.LookAt(followEnemy.transform.position);
            }
            else attackingMode = false;
        }
        else attackingMode = false;
    }
    void animationControl()
    {
        if (agent.velocity.sqrMagnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && energy>0 || Input.GetKey(KeyCode.RightShift) && energy > 0)
            {
                agent.speed = 7f;
                anim.SetFloat("Speed", 2);
                energy -= 10 * Time.deltaTime;
            }
            else
            {
                agent.speed = 3.5f;
                anim.SetFloat("Speed", 1);
                if (energy < maxEnergy && energy > maxEnergy/10)
                {
                    energy += 5 * Time.deltaTime;
                }
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
            if (energy < maxEnergy)
            {
                energy += 5 * Time.deltaTime;
            }
            if (attackingMode)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
    }

    void magic()
    {
        if (mana < 0) mana = 0;
        if (mana > maxMana) mana = maxMana;
        if (mana < maxMana) mana += 7 * Time.deltaTime;

        if(mana>= 10 && Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickMask))
            {
               mana -= 10;
               attackingMode = true;
               agent.velocity = Vector3.zero;
                Vector3 lookVector = new Vector3(hit.point.x, 12, hit.point.z);
               transform.LookAt(lookVector);
               GameObject Bullet = Instantiate(magicBall, firePoint.position, firePoint.rotation);
            }
        }
        if (mana >= 30 && Input.GetMouseButtonDown(2))
        {
            healfh += 50;
            mana -= 30;
        }
    }













    public void DeductHealth(float damage)
    {
        healfh -= damage;
        if (healfh <= 0)
        {
            agent.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0);
            anim.SetBool("Attack", false);
            anim.SetBool("Dead", true);
        }
    }

    void activeAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void disactiveAttackPoint()
    {
        attackPoint.SetActive(false);
        EnemyPoint = null;
        followEnemy = null;

    }


}//end
