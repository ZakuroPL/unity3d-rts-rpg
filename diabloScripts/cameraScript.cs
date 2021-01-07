using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    private float zoom = 1f;
    public float zoomSpeed, minZoom, maxZomm;

    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, minZoom, maxZomm);

        transform.position = player.position - offset * zoom;
    }
}
