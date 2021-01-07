using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPointScript : MonoBehaviour
{

    public Transform FirePoint;
    public GameObject BulletPrifab;
    public int timeBall;
    private bool timer;


    private void Start()
    {
        timer = true;
        GameObject Bullet = Instantiate(BulletPrifab, FirePoint.position, FirePoint.rotation);
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
            yield return new WaitForSeconds(timeBall);
            timer = true;
            Shoot();
        }
    }
    void Shoot()
    {

        GameObject Bullet = Instantiate(BulletPrifab, FirePoint.position, FirePoint.rotation);
    }
}
