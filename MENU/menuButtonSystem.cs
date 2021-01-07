using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuButtonSystem : MonoBehaviour
{
    public GameObject welcomeText;
    void Awake()
    {
        welcomeText.GetComponent<Text>().text = "Welcome " + PlayerPrefs.GetString("playerName");
    }

    public void ballsWorld()
    {
        SceneManager.LoadScene(2);
    }
    public void dragonWorld()
    {
        SceneManager.LoadScene(4);
    }
    public void pedestrianAgent()
    {
        SceneManager.LoadScene(5);
    }
    public void rtsBattle()
    {
        SceneManager.LoadScene(6);
    }
    public void HackAndSlash()
    {
        SceneManager.LoadScene(7);
    }
    public void aboutMe()
    {
        SceneManager.LoadScene(8);
    }


}
