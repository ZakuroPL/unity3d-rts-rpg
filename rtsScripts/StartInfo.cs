using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartInfo : MonoBehaviour
{
    private GameObject info;
    void Awake()
    {
        info = this.gameObject;
        info.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            info.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void clickForDisactive()
    {
        info.SetActive(false);
        Time.timeScale = 1f;
    }
}
