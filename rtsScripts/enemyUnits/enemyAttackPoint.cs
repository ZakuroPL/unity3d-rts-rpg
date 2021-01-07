using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackPoint : MonoBehaviour
{
    public float damage;
    public LayerMask unitLayer;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, unitLayer);
        if (hits.Length > 0)
        {
            if (hits[0].gameObject.tag == "Player")
            {
                hits[0].gameObject.GetComponent<Unit>().DeductHealth(damage);
            }
        }
    }
}
