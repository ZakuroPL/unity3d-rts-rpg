using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAttack : MonoBehaviour
{
    public float damage;
    public LayerMask unitLayer;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, unitLayer);
        if (hits.Length > 0)
        {
            if(hits [0].gameObject.tag == "Enemy")
            {
                hits[0].gameObject.GetComponent<Enemy>().DeductHealth(damage);
            }
        }
    }
}
