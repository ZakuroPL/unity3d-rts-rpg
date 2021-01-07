using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class barsScript : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;
    public Slider energyBar;
    public static int enemyToKill;

    public GameObject text;

    public GameObject win;
    public GameObject lose;

    private void Awake()
    {
        enemyToKill = 20;

        win.SetActive(false);
        lose.SetActive(false);

    }
    void Update()
    {
        healthBar.value = playerScript.healthProcent;
        manaBar.value = playerScript.mana / playerScript.maxMana;
        energyBar.value = playerScript.energy/playerScript.maxEnergy;

        text.GetComponent<Text>().text = "" + enemyToKill;

        if(enemyToKill <= 0)
        {
            win.SetActive(true);
            Time.timeScale = 0;
        }
        if(playerScript.healthProcent <= 0)
        {
            StartCoroutine (youLose());
            IEnumerator youLose()
            {
                yield return new WaitForSeconds(1f);
                lose.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void tryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
