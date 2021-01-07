using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicBall : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myBody.MovePosition(transform.position + transform.forward * speed);
        StartCoroutine(delete());
        IEnumerator delete()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
