using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBuilding : MonoBehaviour
{

    public GameObject buildingPanel;

    void Awake()
    {
        buildingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activeMe()
    {
        buildingPanel.SetActive(true);
    }
}
