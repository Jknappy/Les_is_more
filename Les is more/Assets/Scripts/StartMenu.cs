using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string Level_One = "Level_One";

    public bool start_les = false;
    public bool chose_les = false;
    public bool chose_sleepy = false;
    public bool chose_angry = false;

    public void StartLes()
    {
        start_les = true;
        //SceneManager.LoadScene(Level_One);       
    }

    public void Selected_Les()
    {
        chose_les = true;
    }

    public void Selected_Sleepy()
    {
        chose_sleepy = true;
    }

    public void Selected_Angry()
    {
        chose_angry = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
