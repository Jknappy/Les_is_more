using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string Level_One = "Level_One";

    public void StartLes()
    {
        SceneManager.LoadScene(Level_One);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
