using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class aboutMeScript : MonoBehaviour
{
    public GameObject aboutmeText;
    public GameObject skillsText;
    public GameObject jobsText;
    public GameObject hobbiesText;
    public GameObject futureText;
    public GameObject contactText;

    public GameObject foto;

    void Awake()
    {
        foto.SetActive(true);
        aboutmeText.SetActive(false);
        skillsText.SetActive(false);
        jobsText.SetActive(false);
        hobbiesText.SetActive(false);
        futureText.SetActive(false);
        contactText.SetActive(false);
    }
    
    public void clickAbout()
    {
        aboutmeText.SetActive(true);
        skillsText.SetActive(false);
        jobsText.SetActive(false);
        hobbiesText.SetActive(false);
        futureText.SetActive(false);
        contactText.SetActive(false);
        foto.SetActive(false);
    }
    public void clickSkills()
    {
        aboutmeText.SetActive(false);
        skillsText.SetActive(true);
        jobsText.SetActive(false);
        hobbiesText.SetActive(false);
        futureText.SetActive(false);
        contactText.SetActive(false);
        foto.SetActive(false);
    }
    public void clickJobs()
    {
        aboutmeText.SetActive(false);
        skillsText.SetActive(false);
        jobsText.SetActive(true);
        hobbiesText.SetActive(false);
        futureText.SetActive(false);
        contactText.SetActive(false);
        foto.SetActive(false);
    }
    public void clickhobbies()
    {
        aboutmeText.SetActive(false);
        skillsText.SetActive(false);
        jobsText.SetActive(false);
        hobbiesText.SetActive(true);
        futureText.SetActive(false);
        contactText.SetActive(false);
        foto.SetActive(false);
    }
    public void clickfuture()
    {
        aboutmeText.SetActive(false);
        skillsText.SetActive(false);
        jobsText.SetActive(false);
        hobbiesText.SetActive(false);
        futureText.SetActive(true);
        contactText.SetActive(false);
        foto.SetActive(false);
    }
    public void clickContact()
    {
        aboutmeText.SetActive(false);
        skillsText.SetActive(false);
        jobsText.SetActive(false);
        hobbiesText.SetActive(false);
        futureText.SetActive(false);
        contactText.SetActive(true);
        foto.SetActive(false);
    }
    public void backBack()
    {
        SceneManager.LoadScene(1);
    }
}
