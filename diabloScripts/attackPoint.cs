using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPoint : MonoBehaviour
{
    public float damage;
    public bool isPlayer;


    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        if (hits.Length > 0)
        {
            if (isPlayer)
            {
                if (hits[0].gameObject.tag == "Enemy")
                {
                    hits[0].gameObject.GetComponent<enemyScriptDiablo>().DeductHealth(damage);
                }
            }
            else
            {
                if (hits[0].gameObject.tag == "Player")
                {
                    hits[0].gameObject.GetComponent<playerScript>().DeductHealth(damage);
                }
            }

        }
    }
}
