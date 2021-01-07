using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class passwordScript : MonoBehaviour
{
    public GameObject inputField;
    public GameObject warning;
    public string password;
    string correctPassword = "hr2020";

    private void Awake()
    {
        warning.SetActive(false);
    }
    private void Update()
    {
        password = inputField.GetComponent<Text>().text;
    }

    public void nameClick()
    {
        if (password != correctPassword)
        {
            warning.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(9);
        }
    }

    public void backtoMenu()
    {
        SceneManager.LoadScene(1);
    }

}//end
