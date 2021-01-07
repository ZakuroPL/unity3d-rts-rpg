using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointScript : MonoBehaviour
{

    public Transform FirePoint;
    public GameObject EnemyPrifab;
    public int timeCreate;
    private bool timer;


    private void Start()
    {
        timer = true;
    }
    void Update()
    {
        if (timer)
        {
            StartCoroutine(newBall());
            timer = false;
        }

        IEnumerator newBall()
        {
            yield return new WaitForSeconds(timeCreate);
            timer = true;
            Shoot();
        }
    }
    void Shoot()
    {
        StartCoroutine(enemy1());
        StartCoroutine(enemy2());
        StartCoroutine(enemy3());
        IEnumerator enemy1()
        {
            yield return new WaitForSeconds(1);
           
            sendEnemy();
        }
        IEnumerator enemy2()
        {
            yield return new WaitForSeconds(2);
            
            sendEnemy();
        }
        IEnumerator enemy3()
        {
            yield return new WaitForSeconds(3);
            
            sendEnemy();
        }
    }
    void sendEnemy()
    {
        GameObject Bullet = Instantiate(EnemyPrifab, FirePoint.position, FirePoint.rotation);
    }
}


