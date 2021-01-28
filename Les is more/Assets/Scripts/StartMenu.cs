using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string Level_One = "Level_One";

    public GameObject game_master;
    public GameMaster gm;

    public void StartLes()
    {
        game_master = GameObject.Find("GameManager");
        gm = game_master.GetComponent<GameMaster>();
        SceneManager.LoadScene(Level_One);       
    }

    public void Selected_Les()
    {
        gm.chose_les = true;
    }

    public void Selected_Sleepy()
    {
        gm.chose_sleepy = true;
    }

    public void Selected_Angry()
    {
        gm.chose_angry = true;
    }

}
