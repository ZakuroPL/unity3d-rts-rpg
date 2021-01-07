using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myBody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        StartCoroutine(delete());
        IEnumerator delete()
        {
            yield return new WaitForSeconds(60f);
            Destroy(gameObject);
        }
    }
}
