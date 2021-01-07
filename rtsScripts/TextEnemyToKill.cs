using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextEnemyToKill : MonoBehaviour
{
    Text text;
    public static int EnemyAmount;
    public static int PlayerAmount;

    public GameObject win;
    public GameObject lose;
    void Awake()
    {
        text = GetComponent<Text>();
        win.SetActive(false);
        lose.SetActive(false);
        EnemyAmount = 54;
        PlayerAmount = 54;
    }


    void Update()
    {
        text.text = "Enemy: " + EnemyAmount;

        if(EnemyAmount <= 0)
        {
            win.SetActive(true);
        }
        if (PlayerAmount <= 0)
        {
            lose.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(1);
        }

    }
    public void back()
    {
        SceneManager.LoadScene(1);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
