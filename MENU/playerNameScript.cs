using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerNameScript : MonoBehaviour
{
    public GameObject inputField;
    public GameObject warning;
    public string Name;

    private void Awake()
    {
        warning.SetActive(false);
        string playerName = PlayerPrefs.GetString("playerName");
    }
    private void Update()
    {
        Name = inputField.GetComponent<Text>().text;
    }

    public void nameClick()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            warning.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("playerName", Name);
            SceneManager.LoadScene(1);
        }
    }

}
